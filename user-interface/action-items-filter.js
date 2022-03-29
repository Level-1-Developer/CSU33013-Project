const drop_btn = document.querySelector(".drop-btn");
const tooltip = document.querySelector(".tooltip");
const menu_wrapper = document.querySelector(".wrapper");
const filter_bar = document.querySelector(".filter-bar");


const time_range_drop = document.querySelector(".time_range-drop");
const status_drop = document.querySelector(".status-drop");
const campaign_drop = document.querySelector(".campaign-drop");
const country_drop = document.querySelector(".country-drop");
const language_drop = document.querySelector(".language-drop");
const customerset_drop = document.querySelector(".customerset-drop");



const time_range_item = document.querySelector(".time_range-item");
const status_item = document.querySelector(".status-item");
const campaign_item = document.querySelector(".campaign-item");
const country_item = document.querySelector(".country-item");
const language_item = document.querySelector(".language-item");
const customerset_item = document.querySelector(".customerset-item");



const time_range_btn = document.querySelector(".back-time_range-btn");
const status_btn = document.querySelector(".back-status-btn");
const campaign_btn = document.querySelector(".back-campaign-btn");
const country_btn = document.querySelector(".back-country-btn");
const language_btn = document.querySelector(".back-language-btn");
const customerset_btn = document.querySelector(".back-customerset-btn");

drop_btn.onclick = (()=>{
    menu_wrapper.classList.toggle("show");
    tooltip.classList.toggle("show");
});

time_range_item.onclick = (()=>{
    filter_bar.style.marginLeft = "-18rem";
    setTimeout(()=>{
        time_range_drop.style.display = "block";
    }, 100);
});

status_item.onclick = (()=>{
    filter_bar.style.marginLeft = "-18rem";
    setTimeout(()=>{
        status_drop.style.display = "block";
    }, 100);
});

campaign_item.onclick = (()=>{
    filter_bar.style.marginLeft = "-18rem";
    setTimeout(()=>{
        campaign_drop.style.display = "block";
    }, 100);
});

country_item.onclick = (()=>{
    filter_bar.style.marginLeft = "-18rem";
    setTimeout(()=>{
        country_drop.style.display = "block";
    }, 100);
});

language_item.onclick = (()=>{
    filter_bar.style.marginLeft = "-18rem";
    setTimeout(()=>{
        language_drop.style.display = "block";
    }, 100);
});

customerset_item.onclick = (()=>{
    filter_bar.style.marginLeft = "-18rem";
    setTimeout(()=>{
        customerset_drop.style.display = "block";
    }, 100);
});



time_range_btn.onclick = (()=>{
    filter_bar.style.marginLeft = "0px";
    time_range_drop.style.display = "none";

});

status_btn.onclick = (()=>{
    filter_bar.style.marginLeft = "0px";
    status_drop.style.display = "none";
});

campaign_btn.onclick = (()=>{
    filter_bar.style.marginLeft = "0px";
    campaign_drop.style.display = "none";
});

country_btn.onclick = (()=>{
    filter_bar.style.marginLeft = "0px";
    country_drop.style.display = "none";
});


language_btn.onclick = (()=>{
    filter_bar.style.marginLeft = "0px";
    language_drop.style.display = "none";
});

customerset_btn.onclick = (()=>{
    filter_bar.style.marginLeft = "0px";
    customerset_drop.style.display = "none";
});


function mainSearch(field) {
    const filter = field.value.toUpperCase();
    const table = document.querySelector('table');
    const tr = table.querySelectorAll('tr');
    let i, txtValue, td, ts;
    if (filter.length > 0) {
        for (i = 0; i < tr.length; i++) {
            td = tr[i].querySelector("td:nth-child(1)");
            ts = tr[i].querySelector("td:nth-child(2)");
            if (td) {
                txtValue = td.textContent || td.innerText ||
                    ts.textContent || ts.innerText;
                if (txtValue.toUpperCase().indexOf(filter) > -1) {
                    tr[i].style.display = "";
                } else {
                    tr[i].style.display = "none";
                }
            }
        }
    }
}
function convertedStatus(){
    const filter = "CONVERTED";
    const table = document.querySelector('table');
    const tr = table.querySelectorAll('tr');
    let i, txtValue, td;

    for (i = 0; i < tr.length; i++) {
        td = tr[i].querySelector("td:nth-child(3)");
        if (td) {
            txtValue = td.textContent || td.innerText ;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}

function purchasedStatus(){
    const filter = "PURCHASED";
    const table = document.querySelector('table');
    const tr = table.querySelectorAll('tr');
    let i, txtValue, td;

    for (i = 0; i < tr.length; i++) {
        td = tr[i].querySelector("td:nth-child(3)");
        if (td) {
            txtValue = td.textContent || td.innerText ;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}

function saved(){
    const filter = "SAVED-CART";
    const table = document.querySelector('table');
    const tr = table.querySelectorAll('tr');
    let i, txtValue, td;

    for (i = 0; i < tr.length; i++) {
        td = tr[i].querySelector("td:nth-child(4)");
        if (td) {
            txtValue = td.textContent || td.innerText ;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}

function abandoned(){
    const filter = "ABANDONED-CART";
    const table = document.querySelector('table');
    const tr = table.querySelectorAll('tr');
    let i, txtValue, td;

    for (i = 0; i < tr.length; i++) {
        td = tr[i].querySelector("td:nth-child(4)");
        if (td) {
            txtValue = td.textContent || td.innerText ;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}



function countrySearch(field) {
    const filter = field.value.toUpperCase();
    const table = document.querySelector('table');
    const tr = table.querySelectorAll('tr');
    let i, txtValue, td;
    if (filter.length > 0) {
        for (i = 0; i < tr.length; i++) {
            td = tr[i].querySelector("td:nth-child(6)");
            if (td) {
                txtValue = td.textContent || td.innerText ;
                if (txtValue.toUpperCase().indexOf(filter) > -1) {
                    tr[i].style.display = "";
                } else {
                    tr[i].style.display = "none";
                }
            }
        }
    }
}

function languageSearch(field) {
    const filter = field.value.toUpperCase();
    const table = document.querySelector('table');
    const tr = table.querySelectorAll('tr');
    let i, txtValue, td;
    if (filter.length > 0) {
        for (i = 0; i < tr.length; i++) {
            td = tr[i].querySelector("td:nth-child(7)");
            if (td) {
                txtValue = td.textContent || td.innerText ;
                if (txtValue.toUpperCase().indexOf(filter) > -1) {
                    tr[i].style.display = "";
                } else {
                    tr[i].style.display = "none";
                }
            }
        }
    }
}

function customersetSearch(field) {
    const filter = field.value.toUpperCase();
    const table = document.querySelector('table');
    const tr = table.querySelectorAll('tr');
    let i, txtValue, td;
    if (filter.length > 0) {
        for (i = 0; i < tr.length; i++) {
            td = tr[i].querySelector("td:nth-child(8)");
            if (td) {
                txtValue = td.textContent || td.innerText ;
                if (txtValue.toUpperCase().indexOf(filter) > -1) {
                    tr[i].style.display = "";
                } else {
                    tr[i].style.display = "none";
                }
            }
        }
    }
}
