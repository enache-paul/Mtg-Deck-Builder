export { getData };

const getData = async(endpoint) => {

    return fetch(endpoint, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(data => {
            return data;
        })
        .catch((error) => {
        console.error('Error:', error);
    });
}