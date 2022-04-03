async function getCamerasData(name) {
    const response = await fetch(`https://localhost:7166/api/Cameras/GetCameras?name=${name}`);
    const data = await response.text();
    const parsedData = JSON.parse(data);

    return parsedData.map(x => {
        return {
            ...x,
            columnId: getColumnIndexByNumber(x.number)
        }
    });
}

function getColumnIndexByNumber(number) {

    if (number % 5 === 0 && number % 3 === 0) {
        return 'column15';
    } else if (number % 3 === 0) {
        return 'column3';
    } else if (number % 5 === 0) {
        return 'column5';
    }

    return 'columnOther';
}

function appendCameraDataToHtmlTable(cameraEntry) {
    const tableRef = document.getElementById(cameraEntry.columnId);

    const newRow = tableRef.insertRow(-1);

    addCell(cameraEntry, 0, newRow, 'number');
    addCell(cameraEntry, 1, newRow, 'name');
    addCell(cameraEntry, 2, newRow, 'latitude');
    addCell(cameraEntry, 3, newRow, 'longitude');
}

function addCell(cameraEntry, index, row, name) {
    const cell = row.insertCell(index);
    const node = document.createTextNode(cameraEntry[name]);
    cell.appendChild(node);
}

function prepareMap(data) {

    const map = L.map('map').setView([52.0914, 5.1115], 13);

    L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token={accessToken}', {
        attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
        maxZoom: 18,
        id: 'mapbox/streets-v11',
        tileSize: 512,
        zoomOffset: -1,
        accessToken: 'pk.eyJ1IjoibWljaGFsMTNtIiwiYSI6ImNsMWpmMW44czEyd2wzZ3F1NWFvdWRsNDAifQ.G70rtvCAyhVhyVMvVIwPEA'
    }).addTo(map);

    data.forEach(e => L.marker([e.latitude, e.longitude]).addTo(map))
}

getCamerasData('').then(data => {

    data.forEach(d => appendCameraDataToHtmlTable(d));

    prepareMap(data);

});



