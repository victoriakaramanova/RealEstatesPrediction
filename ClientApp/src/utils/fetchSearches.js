import authService from '../components/api-authorization/AuthorizeService';


const fetchSearches = async (userId) => {
    const token = await authService.getAccessToken();
    const promise = await fetch(`search/get?userId=${userId}`, {
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json; charset=utf-8',
            'Authorization': !token ? {} : `Bearer ${token}`,
        }
    });
    if (!promise.ok) {
        console.warn(XMLHttpRequest)
    } else {
        const searches = await promise.json();
        return searches;
    }
}

export default fetchSearches;
