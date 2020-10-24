import authService from '../components/api-authorization/AuthorizeService';

const fetchEstates = async (id) => {

    console.log(id);
    const token = await authService.getAccessToken();
    const promise = await fetch(`near/post?id=${id}`, {
        //method: 'POST',
        mode: 'cors',
        //body: JSON.stringify({ id }),
        headers: {
            'content-type': 'application/json; charset=utf-8',
            'authorization': !token ? {} : `bearer ${token}`,
        }
    });
    if (!promise.ok) {
        console.warn(XMLHttpRequest)
    } else {
        const estates = await promise.json();
        console.log(estates)
        return estates;
    }
}

export default fetchEstates;
