async function joinCourse(courseId) {
    const result = await api('POST', 'courseusers', { courseId});
    const message = result.status == 200 ? await result.text() : (messages[result.status] ?? messages.default);

    alertify.alert("Message", message, function(){
        alertify.message(message);
    });
}

async function updateStatus(courseId, userId, status) {
    const result = await api('PUT', 'courseusers', { courseId, userId, status});
    const message = result.status == 200 ? await result.text() : (messages[result.status] ?? messages.default);

    alertify.alert(result.successfull ? "Successfull" : "Error", message, function(){
        alertify.message(message);
    });
}

async function api(method, endpoint, data) {
    const options = { method }
    const headers = {}

    if (data !== undefined) {
        headers['Content-Type'] = 'application/json'
        options.body = JSON.stringify(data);
    }

    options.headers = headers;

    return await fetch(`/api/${endpoint}`, options);
}

const messages = {
    401: 'Please log in',
    default: 'Something went wrong'
}