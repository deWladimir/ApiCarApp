const uri = 'api/BodyTypes';
let bodytypes = [];

function getBodyTypes() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayBodyTypes(data))
        .catch(error => console.error('Unable to get BodyTypes.', error));
}

function addBodyType() {
    const addNameTextbox = document.getElementById('add-name');
    const addInfoTextbox = document.getElementById('add-info');

    const bodytype = {
        name: addNameTextbox.value.trim(),
        info: addInfoTextbox.value.trim(),
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(bodytype)
    })
        .then(response => response.json())
        .then(() => {
            getBodyTypes();
            addNameTextbox.value = '';
            addInfoTextbox.value = '';
        })
        .catch(error => console.error('Unable to add the BodyType.', error));
}

function deleteBodyType(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getBodyTypes())
        .catch(error => console.error('Unable to delete the BodyType.', error));
}

function displayEditForm(id) {
    const bodytype = bodytypes.find(bodytype => bodytype.id === id);

    document.getElementById('edit-id').value = bodytype.id;
    document.getElementById('edit-name').value = bodytype.name;
    document.getElementById('edit-info').value = bodytype.info;
    document.getElementById('editForm').style.display = 'block';
}

function updateBodyType() {
    const bodytypeId = document.getElementById('edit-id').value;
    const bodytype = {
        id: parseInt(bodytypeId, 10),
        name: document.getElementById('edit-name').value.trim(),
        info: document.getElementById('edit-info').value.trim()
    };

    fetch(`${uri}/${bodytypeId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(bodytype)
    })
        .then(() => getBodyTypes())
        .catch(error => console.error('Unable to update the BodyType.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}


function _displayBodyTypes(data) {
    const tBody = document.getElementById('bodytypes');
    tBody.innerHTML = '';


    const button = document.createElement('button');

    data.forEach(bodytype => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Редагувати';
        editButton.setAttribute('onclick', `displayEditForm(${bodytype.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Видалити';
        deleteButton.setAttribute('onclick', `deleteBodyType(${bodytype.id})`);

        let tr = tBody.insertRow();


        let td1 = tr.insertCell(0);
        let textNode = document.createTextNode(bodytype.name);
        td1.appendChild(textNode);

        let td2 = tr.insertCell(1);
        let textNodeInfo = document.createTextNode(bodytype.info);
        td2.appendChild(textNodeInfo);

        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });

    bodytypes = data;
}
