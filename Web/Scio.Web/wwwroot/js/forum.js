async function toggleComments(postId) {
    const comments = document.getElementById(postId).querySelector(".forum-post-comments");
    const btn = document.getElementById(postId).querySelector(".forum-comments-toggle");

    if (comments.style.maxHeight) {
        comments.style.maxHeight = null;
        btn.innerHTML = `comments ${icons.plus}`;
    } else {
        if (comments.childElementCount == 1) {
            btn.innerHTML = `comments ${icons.spinner}`;
            
            const result = await forumApi('GET', `comments?postId=${postId}`);
            if (result.successfull) {
                result.data.forEach(comment => comments.insertBefore(createComment(comment), comments.lastElementChild));
            }else{
                alertify.alert("Error", result.message, function(){
                    alertify.message(result.message);
                });
            }  
        }

        btn.innerHTML = `comments ${icons.minus}`;
        comments.style.maxHeight = comments.scrollHeight + "px";
    }
}

async function postComment(postId) {
    const currentPost = document.getElementById(postId);
    const comments = currentPost.querySelector(".forum-post-comments");
    const commentsCount = currentPost.querySelector(".forum-comments-count");
    const textArea = comments.querySelector("textarea");
    const button = comments.querySelector("button");
    button.disabled = true;
    button.innerHTML = `Send ${icons.spinner}`;

    const result = await forumApi('POST', 'comments', { postId, body: textArea.value });

    if(result.successfull){
        textArea.value = "";
        comments.insertBefore(createComment(result.data), comments.lastElementChild);
        comments.style.maxHeight = comments.scrollHeight + "px";
        commentsCount.textContent = Number(commentsCount.textContent) + 1; //Change
    }

    button.innerHTML = `Send ${icons.send}`;
    button.disabled = false;

    alertify.alert(result.successfull ? "Successfull" : "Error", result.message, function(){
        alertify.message(result.message);
    });
}

async function vote(voteValue, postId) {
    const forumPost = document.getElementById(postId);
    const vote = voteType[voteValue];
    const target = forumPost.querySelector(`.${vote}`);
    target.innerHTML = icons.spinner;

    const result = await forumApi('POST', 'votes', { voteValue, postId });
    if (result.successfull) {
        const votes = forumPost.querySelector('.forum-votes-count');
        votes.textContent = result.data;
    }

    target.innerHTML = icons[vote];

    alertify.alert(result.successfull ? "Successfull" : "Error", result.message, function(){
        alertify.message(result.message);
    });
}

async function forumApi(method, endpoint, data) {
    const options = { method }
    const headers = {}

    if (data !== undefined) {
        headers['Content-Type'] = 'application/json'
        options.body = JSON.stringify(data);
    }

    options.headers = headers;

    const response = await fetch(`/api/${endpoint}`, options);

    if (response.status == 401) {
        return { message: messages.login }
    }

    const result = await response.json();

    if (response.status == 200) {
        result.successfull = true;
    } else {
        if (result.ErrorMessage) {
            result.message = result.ErrorMessage[0]; //FIX
        } else if (result.errors) {
            result.message = result.errors[Object.keys(result.errors)[0]].join(' '); //FIX
        }
    }
    
    return result;
}

const icons = {
    spinner: '<i class="fas fa-spinner i-spinner"></i>',
    downvote: '<i class="fas fa-chevron-down"></i>',
    upvote: '<i class="fas fa-chevron-up"></i>',
    plus: '<i class="fas fa-plus"></i>',
    minus: '<i class="fas fa-minus"></i>',
    send: '<i class="fas fa-paper-plane"></i>'
}

const messages = {
    defaultError: 'Something went wrong',
    login: 'Please login to your account'
}

const voteType = { '-1': 'downvote', '1': 'upvote' }

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
        createElement('span', ` - ${comment.authorEmail} `),
        createElement('span', comment.createdOn)
    ], { className: 'forum-comment' }
    );
}