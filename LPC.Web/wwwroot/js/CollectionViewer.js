const bs = document.getElementById('baseContainer');
const urlParams = new URLSearchParams(window.location.search);
const collectionType = urlParams.get('collectiontype');
console.log(collectionType, 'collectionType');

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

            artist.innerText = record.artist;
            album.innerText = record.album;

            block.appendChild(img);
            block.appendChild(artist);
            block.appendChild(album);
            bs.appendChild(block);
        }
        ))
})();
