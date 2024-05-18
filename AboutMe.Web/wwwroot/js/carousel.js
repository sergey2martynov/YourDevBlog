let currentMediaIndex = 0;
const mediaItems = document.querySelectorAll('.carousel-item');

function showMedia(index) {
    mediaItems.forEach((item, i) => {
        item.classList.toggle('active', i === index);
    });
}

function nextMedia() {
    pauseCurrentMedia();
    currentMediaIndex = (currentMediaIndex + 1) % mediaItems.length;
    showMedia(currentMediaIndex);
}

function prevMedia() {
    pauseCurrentMedia();
    currentMediaIndex = (currentMediaIndex - 1 + mediaItems.length) % mediaItems.length;
    showMedia(currentMediaIndex);
}

function pauseCurrentMedia() {
    const currentItem = mediaItems[currentMediaIndex];
    const video = currentItem.querySelector('video');
    if (video) {
        video.pause();
    }
}

showMedia(currentMediaIndex);