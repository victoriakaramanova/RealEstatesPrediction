using ImotiPrediction.Data;
using ImotiPrediction.Models;
using ImotiPrediction.ViewModels;
using Microsoft.AspNetCore.Connections;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers.LightGbm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ImotiPrediction.Services
{
    public static class Prediction 
    {
        public static async Task Main(ApplicationDbContext dbContext)
        {
            //var dbContext = new ApplicationDbContext();
           
            ImotBgDataGatherer t = new ImotBgDataGatherer(dbContext);
           // await t.GatherData(30, 32);
            Console.OutputEncoding = Encoding.UTF8;
            
            var modelFile = "SofiaPropertiesModel.zip";
            if (!File.Exists(modelFile))
            {
                TrainModel(dbContext, modelFile);
            }
            var testModelData = new ModelInput
            {
                BuildingType = "2-СТАЕН",
                District = "град София, Надежда 1",
                Floor = 2,
                TotalFloors = 9,
                Size = 58,
                Year = 2014,
                Type = "Тухла",
            };
              
                testModelData.Dump();

            TestModel(testModelData);
        
        }

        public static float TestModel(ModelInput testModelData)
        {
            var modelFile = "SofiaPropertiesModel.zip";
            var context = new MLContext();
            var model = context.Model.Load(modelFile, out _);
            var predictionEngine = context.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);
            float score = 0;
            //foreach (var testData in testModelData)
            //{
                var prediction = predictionEngine.Predict(testModelData);
                score = prediction.Score;
            return score;
                Console.WriteLine(new string('-', 60));
                Console.WriteLine($"Input: {testModelData.Dump()}");
                Console.WriteLine($"Prediction: {prediction.Score}");
            //}
        }

            private static void TrainModel(ApplicationDbContext dbContext, string modelFile)
        {
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
        }

        public static string Dump(this object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.None);
        }
    }

        
}

