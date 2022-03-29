const drop_btn = document.querySelector(".drop-btn");
const tooltip = document.querySelector(".tooltip");
const menu_wrapper = document.querySelector(".wrapper");
const filter_bar = document.querySelector(".filter-bar");

const campaign_drop = document.querySelector(".campaign-drop");
const forward_status_drop = document.querySelector(".forward_status-drop");
const completed_at_time_drop = document.querySelector(".completed_at_time-drop");

const campaign_item = document.querySelector(".campaign-item");
const forward_status_item = document.querySelector(".forward_status-item");
const completed_at_time_item = document.querySelector(".completed_at_time-item");

const campaign_btn = document.querySelector(".back-campaign-btn");
const forward_status_btn = document.querySelector(".back-forward_status-btn");
const completed_at_time_btn = document.querySelector(".back-completed_at_time-btn");


drop_btn.onclick = (()=>{
    menu_wrapper.classList.toggle("show");
    tooltip.classList.toggle("show");
});

campaign_item.onclick = (()=>{
    filter_bar.style.marginLeft = "-18rem";
    setTimeout(()=>{
        campaign_drop.style.display = "block";
    }, 100);
});

forward_status_item.onclick = (()=>{
    filter_bar.style.marginLeft = "-18rem";
    setTimeout(()=>{
        forward_status_drop.style.display = "block";
    }, 100);
});

completed_at_time_item.onclick = (()=>{
    filter_bar.style.marginLeft = "-18rem";
    setTimeout(()=>{
        completed_at_time_drop.style.display = "block";
    }, 100);
});


campaign_btn.onclick = (()=>{
    filter_bar.style.marginLeft = "0px";
    campaign_drop.style.display = "none";

});

forward_status_btn.onclick = (()=>{
    filter_bar.style.marginLeft = "0px";
    forward_status_drop.style.display = "none";
});

completed_at_time_btn.onclick = (()=>{
    filter_bar.style.marginLeft = "0px";
    completed_at_time_drop.style.display = "none";
});

function saved(){
    var filter = "SAVED-CART";
    var table = document.querySelector('table');
    var tr = table.querySelectorAll('tr');
    var i, txtValue, td;

    for (i = 0; i < tr.length; i++) {
        td = tr[i].querySelector("td:nth-child(2)");
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
    var filter = "ABANDONED-CART";
    var table = document.querySelector('table');
    var tr = table.querySelectorAll('tr');
    var i, txtValue, td;

    for (i = 0; i < tr.length; i++) {
        td = tr[i].querySelector("td:nth-child(2)");
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

function forwarded(){
    var filter = "FORWARDED";
    var table = document.querySelector('table');
    var tr = table.querySelectorAll('tr');
    var i, txtValue, td;

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

function waiting(){
    var filter = "WAITING";
    var table = document.querySelector('table');
    var tr = table.querySelectorAll('tr');
    var i, txtValue, td;

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



