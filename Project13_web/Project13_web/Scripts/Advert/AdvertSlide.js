
const carouselContainer = document.querySelector('.carousel-container');
let currentIndex = 0;

function showImage(index) {
    const newPosition = -index * 100 + '%';
    carouselContainer.style.transform = `translateX(${newPosition})`;
}

function nextSlide() {
    currentIndex = (currentIndex + 1) % 3;
    showImage(currentIndex);
}

function prevSlide() {
    currentIndex = (currentIndex - 1 + 3) % 3;
    showImage(currentIndex);
}

setInterval(nextSlide, 3000); // เปลี่ยนรูปทุก 3 วินาที
