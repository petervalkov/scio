function toggleComments(event) {
    const comments = event.target.closest(".forum-post-main").querySelector(".forum-comments");
    if (comments.style.maxHeight) { comments.style.maxHeight = null; }
    else { comments.style.maxHeight = comments.scrollHeight + "px"; }
}

async function postComment(postId, event) {
    const button = event.target;
    button.disabled = true;
    
    const comments = button.closest(".forum-comments");
    const textArea = comments.querySelector("textarea");
    const commentsCount = button.closest(".forum-post").querySelector(".forum-comments-count");

    const response = await fetch('/api/comments', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ postId, body: textArea.value })
    });

    if(response.status == 200){
        const data = await response.json();
        const forumComment = createElement('div', [
            createElement('p', data.body),
            createElement('span', data.createdOn)
        ], { className:'forum-comment-content' });

        textArea.value = "";
        comments.insertBefore(forumComment, comments.lastElementChild);
        comments.style.maxHeight = comments.scrollHeight + "px";
        commentsCount.textContent = Number(commentsCount.textContent) + 1;
    } else {
        alert("Something went wrong!")
    }

    button.disabled = false;
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
        if (typeof node === 'string') {
            node = document.createTextNode(node);
        }
        result.appendChild(node);
    }

    return result;
}