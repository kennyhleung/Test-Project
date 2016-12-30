getPatientObservations(1);

function getPatientObservations(patientId) {
    $.ajax("http://localhost:57802/api/Observation?subject=Patient/" + patientId, { success: processPatientObservationResponse, accepts: "application/json; charset=utf-8" });
}

function processPatientObservationResponse(response) {
    var observationList = [];
    var observationListForGraphing = [];
    for (i = 0; i < response.entry.length; i++) {
        observationList.push([new Date(response.entry[i].resource.effectiveInstant), response.entry[i].resource.valueInteger]);
        observationListForGraphing.push([new Date(response.entry[i].resource.effectiveInstant).getTime(), response.entry[i].resource.valueInteger]);
    }
    $("#processingDiv").hide();
    showChart(observationListForGraphing);
    showDataTable(observationList);
}

function showDataTable(data) {
    $("#vitalstable").DataTable({
        data: data,
        "info": false,
        "paging": false,
        searching: false,
        order: [[0, "desc"]],
        columns: [{ "title": "Date" },
            { "title": "Weight (lbs)" }]
    });
}

function showChart(data) {
    Highcharts.setOptions({
        global: {
            useUTC: false
        }
    });

    Highcharts.chart('vitalschart', {
        chart: {
            type: 'spline'
        },
        credits: {
            enabled: false
        },
        title: {
            text: 'Body Weight Tracking'
        },
        xAxis: {
            type: 'datetime',
            dateTimeLabelFormats: {
                day: '%b %e'
            },
            title: {
                text: 'Date'
            }
        },
        legend: {
            enabled:false
        },
        yAxis: {
            title: {
                text: 'Weight (lbs)'
            }
        },
        plotOptions: {
            spline: {
                marker: {
                    enabled: true
                }
            }
        },
        tooltip: {
            valueSuffix: " lbs"
        },
        series: [{ name: "Weight", data: data }]
    })
};