async function toggleComments(postId) {
    const comments = document.getElementById(postId).querySelector(".forum-comments");
    if (comments.style.maxHeight) {
        comments.style.maxHeight = null;
    } else {
        if (comments.childElementCount == 1) {
            const data = await commentsApi.get(postId);
            if (data) {
                data.comments.forEach(comment => comments.insertBefore(createComment(comment), comments.lastElementChild));
            } else {
                alert("Something went wrong!");
            }
        }

        comments.style.maxHeight = comments.scrollHeight + "px";
    }
}

async function postComment(postId) {
    const currentPost = document.getElementById(postId);

    const comments = currentPost.querySelector(".forum-comments");
    const commentsCount = currentPost.querySelector(".forum-comments-count");
    const textArea = comments.querySelector("textarea");
    const button = comments.querySelector("button");
    button.disabled = true;

    const data = await commentsApi.post(postId, textArea.value);

    if (data) {
        textArea.value = "";
        comments.insertBefore(createComment(data), comments.lastElementChild);
        comments.style.maxHeight = comments.scrollHeight + "px";
        commentsCount.textContent = Number(commentsCount.textContent) + 1; //Change
    } else {
        alert("Something went wrong!");
    }

    button.disabled = false;
}

const commentsApi = {
    get: async (postId) => await fetch(`/api/comments?postId=${postId}`).then(response => {
        if (response.status == 200) {
            return response.json();
        }
    }),
    post: async (postId, body) => await fetch('/api/comments', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ postId, body })
    }).then(response => {
        if (response.status == 200) {
            return response.json();
        }
    })
}

const voteType = { '-1': 'downvote', '1': 'upvote' }
const icons = {
    spinner: '<i class="fas fa-spinner i-spinner"></i>',
    downvote: '<i class="fas fa-chevron-down"></i>',
    upvote: '<i class="fas fa-chevron-up"></i>'
}
const messages = {
    defaultError: 'Something went wrong',
    login: 'Please login to your account'
}

async function vote(voteValue, postId) {
    const forumPost = document.getElementById(postId);
    const vote = voteType[voteValue];
    const target = forumPost.querySelector(`.${vote}`);
    target.innerHTML = icons.spinner;
    
    let message = messages.defaultError;
    const result = await forumApi.vote({ voteValue, postId });
    if (result) {
        if (result.votesSum != undefined) {
            const votes = forumPost.querySelector('.forum-votes-count');
            votes.textContent = result.votesSum;
            message = result.message;
        } else if (result.errorMessage) {
            message = result.errorMessage;
        }
    }

    target.innerHTML = icons[vote];
    alert(message);
}

const forumApi = {
    vote: async (data) => await handleRequest('votes', 'POST', data)
}

async function handleRequest(endpoint, method, data) {
    const options = { method }
    const headers = {}

    if (data !== undefined) {
        headers['Content-Type'] = 'application/json'
        options.body = JSON.stringify(data);
    }

    options.headers = headers;

    const response = await fetch(`/api/${endpoint}`, options);

    if (response.status == 200) {
        return await response.json();
    } else if (response.status == 401) {
        return { errorMessage: messages.login }
    } else {
        const result = await response.json();
        let errorMessage = messages.defaultError;
        if (result.ErrorMessage) {
            errorMessage = result.ErrorMessage[0]; //Show only first
        } else if (result.errors) {
            errorMessage = result.errors[Object.keys(result.errors)[0]];
        }
        return { errorMessage };
    }
}

function createElement(type, content, attributes) {
    const result = document.createElement(type);

    if (attributes !== undefined) {
        Object.assign(result, attributes);
    }

    if (Array.isArray(content)) {
        content.forEach(append);
    } else {
        append(content);
    }

    function append(node) {
        if (node == null) { //Remove
            node = document.createTextNode('');
        }

        if (typeof node === 'string') {
            node = document.createTextNode(node);
        }
        result.appendChild(node);
    }

    return result;
}

function createComment(comment) {
    return createElement('div', [
        comment.body,
        createElement('span', comment.authorEmail),
        createElement('span', comment.createdOn)
    ], { className: 'forum-comment-content' }
    );
}