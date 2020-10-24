import authService from '../components/api-authorization/AuthorizeService';


const fetchSearch = async (searchId) => {
    const token = await authService.getAccessToken();
    const promise = await fetch(`single/get?searchId=${searchId}`, {
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json; charset=utf-8',
            'Authorization': !token ? {} : `Bearer ${token}`,
        }
    });
    if (!promise.ok) {
        console.warn(XMLHttpRequest)
    } else {
        const search = await promise.json();
        return search;
    }
}

export default fetchSearch;