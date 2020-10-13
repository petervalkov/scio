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