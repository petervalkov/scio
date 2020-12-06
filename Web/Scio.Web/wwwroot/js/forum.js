async function toggleComments(postId) {
    const comments = document.getElementById(postId).querySelector(".forum-post-comments");
    const btn = document.getElementById(postId).querySelector(".forum-comments-toggle");

    if (comments.style.maxHeight) {
        comments.style.maxHeight = null;
        btn.innerHTML = `comments ${icons.plus}`;
    } else {
        if (comments.childElementCount == 1) {
            btn.innerHTML = `comments ${icons.spinner}`;

            const result = await api('GET', `comments?postId=${postId}`);
            if (result.status == 200) {
                const data = await result.json();
                data.forEach(comment => comments.insertBefore(createComment(comment), comments.lastElementChild));
            }else{
                alertify.alert("", messages[result.status] ?? messages.default, function(){
                    alertify.message(messages[result.status] ?? messages.default);
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
    const textArea = comments.querySelector("input");
    const button = comments.querySelector("button");
    button.disabled = true;
    button.innerHTML = `Send ${icons.spinner}`;

    const result = await api('POST', 'comments', { postId, body: textArea.value });

    if(result.status == 200){
        const data = await result.json();
        textArea.value = "";
        comments.insertBefore(createComment(data), comments.lastElementChild);
        comments.style.maxHeight = comments.scrollHeight + "px";
        commentsCount.textContent = Number(commentsCount.textContent) + 1; //Change
    }

    alertify.alert("", messages[result.status] ?? messages.default, function(){
        alertify.message(messages[result.status] ?? messages.default);
    });

    button.innerHTML = `Send ${icons.send}`;
    button.disabled = false;
}

async function vote(voteValue, postId) {
    const forumPost = document.getElementById(postId);
    const vote = voteType[voteValue];
    const target = forumPost.querySelector(`.${vote}`);
    target.innerHTML = icons.spinner;

    const response = await api('POST', 'votes', { voteValue, postId });
    let message = messages[response.status] ?? messages.default;

    if (response.status == 200) {
        const result = await response.json()
        if(result.data){
            const votes = forumPost.querySelector('.forum-votes-count');
            votes.textContent = result.data;
        }
        message = result.message;
    }

    alertify.alert("", message, function(){
        alertify.message(message);
    });

    target.innerHTML = icons[vote];
}

const icons = {
    spinner: '<i class="fas fa-spinner i-spinner"></i>',
    downvote: '<i class="fas fa-chevron-down"></i>',
    upvote: '<i class="fas fa-chevron-up"></i>',
    plus: '<i class="fas fa-plus"></i>',
    minus: '<i class="fas fa-minus"></i>',
    send: '<i class="fas fa-paper-plane"></i>'
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
        createElement('img', '', { className: 'img-circle img-sm', src: '/img/default-user.png' }),
        createElement('div', [
            createElement('span', [
                comment.authorEmail,
                createElement('span', comment.createdOn, { className: 'text-muted float-right' })
            ], { className: 'username' }),
            comment.body
        ], { className: 'comment-text' })
    ], { className: 'card-comment' }
    );
}