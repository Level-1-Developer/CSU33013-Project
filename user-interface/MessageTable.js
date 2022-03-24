window.onload = function()
{
    createMessageTable();
}

function createMessageTable() {
    const elements = MessageElements.GetElements();
    const headings = MessageElements.GetHeadings();


    const table = document.createElement("table");

    const hrow = table.insertRow();
    for (let heading of headings) {
        hrow.insertCell(-1).outerHTML = `<th>${heading}</th>`;
    }

    // iterate data, adding rows and cells for each element
    for (let element of elements) {
        const drow = table.insertRow(-1);

        for (let heading of headings) {
            drow.insertCell(-1).innerHTML = element[heading];
        }

    }

    document.getElementById("tablediv").appendChild(table);
}

