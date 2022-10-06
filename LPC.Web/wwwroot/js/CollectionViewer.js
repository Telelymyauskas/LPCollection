const bs = document.getElementById('baseContainer');
const urlParams = new URLSearchParams(window.location.search);
const collectionType = urlParams.get('collectiontype');
console.log(collectionType, 'collectionType');
const header = document.getElementById('header');
header.innerText = collectionType;
(async () => {
    const response = await fetch(`/collection?collectiontype=${collectionType}`)
        .then(response => response.json())
        .then(data => data.forEach(record => {
            console.log('record', record);
            const block = document.createElement('div');
            const img = document.createElement('img');
            const artist = document.createElement('h4');
            const album = document.createElement('h3');

            img.src = record.imgURL;
            img.height = 200;
            img.width = 200;
            img.className = 'block-record'

            artist.innerText = record.artist;
            artist.className ='text-container'
            album.innerText = record.album;
            album.className = 'text-container'

            block.appendChild(img);
            block.appendChild(artist);
            block.appendChild(album);
            block.style.display = 'inline-block'
            bs.appendChild(block);
        }
        ))
})();

const backButton = document.getElementById('back-button');
backButton.addEventListener('click', () => {
    window.location.href = '/Index.html'
})