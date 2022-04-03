const elements = BatchForwardsElements.GetElements();

function getFields(input, field) {
    const output = [];
    for (let i=0; i < input.length ; ++i) {
        const str = input[i][field];
        output.push(str);
    }
    return output;
}

function getTodaysDate(){
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth()+1;

    var yyyy = today.getFullYear();
    if(dd<10){dd='0'+dd} if(mm<10){mm='0'+mm} today = yyyy+'-'+mm+'-'+dd;
    return today;
}

const datapoints = getFields(elements, "total_processed_messages");
const dates = getFields(elements, "completed_at");
const convertedDates = dates.map(date => new Date(date));

const data = {
    labels: dates,
    datasets: [{
        label: 'Messages',
        data: datapoints,
        backgroundColor:
            'rgb(159,207,239)',
        borderColor: 'rgb(18,88,128)',
        borderWidth: 1
    }]
};

// config
const config = {
    type: 'bar',
    data,
    options: {
        scales: {
            x: {
                type: 'time',
                time: {
                    unit: 'day',
                }
            },
            y: {
                beginAtZero: true
            }
        }
    }
};
const Chart1 = new Chart(
    document.getElementById('msgsChart'),
    config
);

function filterDate1(){
    const start= new Date(document.getElementById('start1').value);
    const end= new Date(document.getElementById('end1').value);

    const filterDates = convertedDates.filter(date => date >= start && date <= end)
    Chart1.config.data.labels = filterDates;


    const startArray = convertedDates.indexOf(filterDates[0])
    const endArray = convertedDates.indexOf(filterDates[filterDates.length-1])
    const copydatapoints = [...datapoints];
    copydatapoints.splice(endArray + 1, filterDates.length);
    copydatapoints.splice(0,startArray);
    Chart1.config.data.datasets[0].data = copydatapoints;
    Chart1.update();
}

function resetDate1(){
    Chart1.config.data.labels = convertedDates;
    Chart1.config.data.datasets[0].data = datapoints;
    Chart1.update();
}
