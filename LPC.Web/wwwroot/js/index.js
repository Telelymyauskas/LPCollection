const bs = document.getElementById('baseContainer');
function addRecordToWishlist(event) {
    const button = event.target;
    fetch(`/add-to-wishlist?recordid=${button.name}`, {
        method: "PUT"
    })
        window.alert('Record was successfully added to your wishlist')
}
function addRecordToLibrary(event) {
    console.log("inFunction");
    const button = event.target;
    fetch(`/add-to-library?recordid=${button.name}`, {
        method: "PUT"
    })
    window.alert('Record was successfully added to your library')
}
(async () => {
    const response = await fetch("/homepage")
        .then(response => response.json())
        .then(data => data.forEach(record => {
            console.log('record', record);
            const block = document.createElement('div');
            const img = document.createElement('img');
            const artist = document.createElement('h4');
            const album = document.createElement('h3');
            const wishlistButton = document.createElement('button');
            const libraryButton = document.createElement('button');

            img.src = record.imgURL;
            img.height = 200;
            img.width = 200;

            artist.innerText = record.artist;
            album.innerText = record.album;

            wishlistButton.textContent = 'add to wishlist'
            wishlistButton.name = record.id;
            wishlistButton.addEventListener('click', addRecordToWishlist)

            libraryButton.textContent = 'add to library'
            libraryButton.name = record.id;
            libraryButton.addEventListener('click', addRecordToLibrary)


            block.appendChild(img);
            block.appendChild(artist);
            block.appendChild(album);
            block.appendChild(wishlistButton);
            block.appendChild(libraryButton);
            bs.appendChild(block);
        }
        ))
})();
const wishlistButton = document.getElementById('myWishlist');
wishlistButton.addEventListener('click', () => {
    window.location.href='/CollectionViewer.html'
})