const bs = document.getElementById('baseContainer');
function addRecordToWishlist(event) {
    const button = event.target;
    fetch(`/add-to-wishlist?recordid=${button.name}`, {
        method: "PUT"
    })
        .then(result => {
            console.log('result', result);
            if (result.status === 400) {
                window.alert('It seems like you already have this record in one of your lists');
            } else {
                window.alert('Record was successfully added to your wishlist');
            }
        })
}
function addRecordToLibrary(event) {
    console.log("inFunction");
    const button = event.target;
    fetch(`/add-to-library?recordid=${button.name}`, {
        method: "PUT"
    })
        .then(result => {
            console.log('result', result);
            if (result.status === 400) {
                window.alert('It seems like you already have this record in one of your lists');
            } else {
                window.alert('Record was successfully added to your library');
            }
        })
}
(async () => {
    const response = await fetch("/homepage")
        .then(response => response.json())
        .then(data => data.forEach(record => {
            console.log('record', record);
            const block = document.createElement('div');
            const img = document.createElement('img');
            const artist = document.createElement('h5');
            const album = document.createElement('h4');
            const wishlistButton = document.createElement('button');
            const libraryButton = document.createElement('button');

            img.src = record.imgURL;
            img.height = 200;
            img.width = 200;
            img.className = 'block-record'

            artist.innerText = record.artist;
            artist.className = 'text-container'
            album.innerText = record.album;
            album.className = 'text-container'

            wishlistButton.textContent = '❤'
            wishlistButton.className = 'add-to-wishlist-button'
            wishlistButton.name = record.id;
            wishlistButton.addEventListener('click', addRecordToWishlist)

            libraryButton.textContent = '✚'
            libraryButton.className = 'add-to-library-button'
            libraryButton.name = record.id;
            libraryButton.addEventListener('click', addRecordToLibrary)


            block.appendChild(img);
            block.appendChild(artist);
            block.appendChild(album);
            block.appendChild(wishlistButton);
            block.appendChild(libraryButton);
            block.style.display = 'inline-block'
            bs.appendChild(block);
        }
        ))
})();
const wishlistButton = document.getElementById('myWishlist');
wishlistButton.addEventListener('click', () => {
    window.location.href = '/CollectionViewer.html?collectiontype=wishlist'
})
const libraryButton = document.getElementById('myLibrary');
libraryButton.addEventListener('click', () => {
    window.location.href = '/CollectionViewer.html?collectiontype=library'
})