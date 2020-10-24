using ImotiPrediction.Data;
using ImotiPrediction.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers.LightGbm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImotiPrediction.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class TrainController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public TrainController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        private async Task<StatusCodeResult> TrainModel(ApplicationDbContext dbContext, string modelFile)
        {
            try
            {
                modelFile = "SofiaPropertiesModel.zip";
            if (!System.IO.File.Exists(modelFile))
            {
                await TrainModel(dbContext, modelFile);
            }

            var context = new MLContext();
            var sqlCommand = "SELECT * FROM " + dbContext.Offers;
            var connString = "Server =.; Database = ImotiPrediction; Trusted_Connection = True; MultipleActiveResultSets = true";
            DatabaseSource dataSource = new DatabaseSource(SqlClientFactory.Instance, connString, sqlCommand);
            DatabaseLoader loader = context.Data.CreateDatabaseLoader<ModelInput>();
            IDataView trainingDataView = loader.Load(dataSource);

        // Data process configuration with pipeline data transformations
            var dataProcessPipeline = context.Transforms.Categorical
                .OneHotEncoding(
                new[]
                {
                    new InputOutputColumnPair(nameof(ModelInput.District), nameof(ModelInput.District)),
                    new InputOutputColumnPair(nameof(ModelInput.Type), nameof(ModelInput.Type)),
                    new InputOutputColumnPair(nameof(ModelInput.BuildingType), nameof(ModelInput.BuildingType)),
                }).Append(
                context.Transforms.Concatenate(
                    outputColumnName: "Features",
                    //nameof(ModelInput.Url),
                    nameof(ModelInput.District),
                    nameof(ModelInput.Type),
                    nameof(ModelInput.BuildingType),
                    nameof(ModelInput.Size),
                    nameof(ModelInput.Floor),
                    nameof(ModelInput.TotalFloors),
                    nameof(ModelInput.Year)));

        // Set the training algorithm (GBM = Gradient Boosting Machine)
            var trainer = context.Regression.Trainers.LightGbm(
                new LightGbmRegressionTrainer.Options
            {
                NumberOfIterations = 4000,
                LearningRate = 0.1006953f,
                NumberOfLeaves = 55,
                MinimumExampleCountPerLeaf = 20,
                UseCategoricalSplit = true,
                HandleMissingValue = false,
                MinimumExampleCountPerGroup = 200,
                MaximumCategoricalSplitPointCount = 16,
                CategoricalSmoothing = 10,
                L2CategoricalRegularization = 1,
                Booster = new GradientBooster.Options { L2Regularization = 0.5, L1Regularization = 0 },
                LabelColumnName = nameof(ModelInput.Price),
                FeatureColumnName = "Features",
            });
            var trainingPipeline = dataProcessPipeline.Append(trainer);

            ITransformer model = trainingPipeline.Fit(trainingDataView);
            context.Model.Save(model, trainingDataView.Schema, modelFile);
            
                return StatusCode(200);
            }
            catch (Exception)
            {
                throw new Exception(nameof(Exception));
            }
           
        }
    }
}
