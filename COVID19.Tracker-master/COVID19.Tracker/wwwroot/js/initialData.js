$("#stateSelect2").select2({
    placeholder: "Select a State",
    theme: "bootstrap4",
    ajax: {
        url: "/api/state/search",
        contentType: "application/json; charset=utf-8",
        data: function (params) {
            var queryParameters =
            {
                term: params.term,
            };
            return queryParameters;
        },
        processResults: function (result) {
            return {
                results: $.map(result, function (item) {
                    return {
                        id: item.code,
                        text: item.name,
                        code: item.code
                    };
                }),
            };
        } 
    },
    escapeMarkup: function (m) {
        return m;
    }
}).on('change', async function () {
    data = $("#stateSelect2").select2("data")[0];
    await loadDataByCode(data); fetchChartData(data);
});



$(document).ready(async function () {
    $("#districtsTable").show();
    animateNumbers();
    $("#countryButton").click(async function () {
        $('#stateSelect2').empty();
        var data = { id: "GLOBE", text: "Global" }
        await loadDataByCode(data);
    });
});

function animateNumbers() {
    $('.Count').each(function () {
        $(this).prop('Counter', 0).animate({
            Counter: $(this).text()
        }, {
            duration: 1000,
            easing: 'swing',
            step: function (now) {
                $(this).text(Math.ceil(now).toLocaleString('en'));
            }
        });
    });
}

async function loadData(data) {
    var date = new Date($('#selectedDate').val());
    var url = "/api/stats?code=" + data.id + "&&date=" + moment(date).format("MM-DD-YYYY");
    let response = await fetch(url);

    if (response.ok) {
        let json = await response.json();
        console.log(json);
        $('#countryStateLabel').text(data.text);

        var formattedDate = moment(date).format("YYYY-MM-DD");
        if (json.delta == null) {
            $('#confirmedDelta').text(0);
            $('#recoveredDelta').text(0);
            $('#deceasedDelta').text(0);
        }
        else {
            $('#confirmedDelta').text(json.delta.confirmed);
            $('#recoveredDelta').text(parseInt(json.delta.recovered));
            $('#deceasedDelta').text(parseInt(json.delta.deceased));
        }
        var today = new Date();
        if (today.toDateString() == date.toDateString()) {

            $('#dateLastUpdatedOn').text("Data Last Updated on " + moment(json.meta.last_Updated).format("DD MMMM, YYYY hh:mm:ss A"));
            $('#dateLastUpdatedOn').attr('style', 'background: #1cc88a !important; color: white !important');
        }
        else {
            $('#dateLastUpdatedOn').text("You are viewing the Data for " + moment(date).format("DD MMMM, YYYY"));
            $('#dateLastUpdatedOn').attr('style', 'background: #e74a3b !important;color: white !important');
        }

        $('#confirmedTotal').text(parseInt(json.total.confirmed));
        $('#testedUpdatedDate').text("As of " + moment(json.meta.tested.last_Updated).format("DD MMMM, YYYY"));
        $('#testedTotal').text(json.total.tested);

        $('#recoveredTotal').text(json.total.recovered);

        $('#deceasedTotal').text(json.total.deceased);
        animateNumbers();
        if (json.districts !== null) {
            $("tbody").children().remove();
            for (var key in json.districts) {
                console.log(key);
                $("#districtsTable")
                    .find("tbody")
                    .append(
                        "<tr><td><a>" +
                        key +
                        "</a></td><td><a>" +
                        json.districts[key].total.confirmed +
                        "<small style='margin: 5px;color: white;background: #4e73df;border-radius: 5px;padding: 5px;'> +" + json.districts[key].delta.confirmed + "</small>"
                        + "</a></td><td><a>" +
                        json.districts[key].total.recovered +
                        "<small style='margin: 5px;color: white;background: #36b9cc;border-radius: 5px;padding: 5px;'> +" + json.districts[key].delta.recovered + "</small>"
                        + "</a></td><td><a>" +
                        json.districts[key].total.deceased +
                        "<small style='margin: 5px;color: white;background: #e74a3b;border-radius: 5px;padding: 5px;'> +" + json.districts[key].delta.deceased + "</small>"
                        + "</a></td><td><a>" +
                        "</tr>"
                    );
            }
        }

        //Show Districts

    } else {
        alert("HTTP-Error: " + response.status);

    }
}

async function loadDataByCode(data) {
    var date = new Date($('#selectedDate').val());
    var url = "/api/stats?code=" + data.id;
    let response = await fetch(url);

    if (response.ok) {
        let json = await response.json();
        console.log(json);
        $('#countryStateLabel').text(data.text);

        if (json.newConfirmed == null) {
            $('#confirmedDelta').text(0);
            $('#recoveredDelta').text(0);
            $('#deceasedDelta').text(0);
        }
        else {
            $('#confirmedDelta').text(json.newConfirmed);
            $('#recoveredDelta').text(parseInt(json.newRecovered));
            $('#deceasedDelta').text(parseInt(json.newDeaths));
        }

        $('#dateLastUpdatedOn').text("Data Last Updated on " + moment(json.date).format("DD MMMM, YYYY hh:mm:ss A"));
        $('#dateLastUpdatedOn').attr('style', 'background: #1cc88a !important; color: white !important');

        $('#confirmedTotal').text(parseInt(json.totalConfirmed));
        $('#testedUpdatedDate').text("As of " + moment(json.date).format("DD MMMM, YYYY"));
        $('#testedTotal').text(json.totalRecovered);

        $('#recoveredTotal').text(json.totalRecovered);

        $('#deceasedTotal').text(json.totalDeaths);
        animateNumbers();
        $("#districtsTable").hide()
        if (json.subdivision !== null) {
            /*$("tbody").children().remove();
            for (var key in json.subdivision) {
                console.log(key);
                $("#districtsTable")
                    .find("tbody")
                    .append(
                        "<tr><td><a>" +
                        json.subdivision[key].countryName +
                        "</a></td><td><a>" +
                        json.subdivision[key].totalConfirmed +
                        "<small style='margin: 5px;color: white;background: #4e73df;border-radius: 5px;padding: 5px;'> +" + json.subdivision[key].newConfirmed + "</small>"
                        + "</a></td><td><a>" +
                        json.subdivision[key].totalRecovered +
                        "<small style='margin: 5px;color: white;background: #36b9cc;border-radius: 5px;padding: 5px;'> +" + json.subdivision[key].newRecovered + "</small>"
                        + "</a></td><td><a>" +
                        json.subdivision[key].totalDeaths +
                        "<small style='margin: 5px;color: white;background: #e74a3b;border-radius: 5px;padding: 5px;'> +" + json.subdivision[key].newDeaths + "</small>"
                        + "</a></td><td><a>" +
                        "</tr>"
                    );
            }*/
            $("#districtsTable").show();
            $('#countryTable').DataTable({
                data: json.subdivision,
                columns: [
                    { data: 'countryName' },
                    { data: 'totalConfirmed' },
                    { data: 'newConfirmed' },
                    { data: 'totalRecovered' },
                    { data: 'newRecovered' },
                    { data: 'totalDeaths' },
                    { data: 'newDeaths' },
                ],
                rowCallback: function (row, data, index) {
                    if (data[2] > 100) {
                        $(row).find('td:eq(3)').css('color', 'red');
                    }
                }
            }
            ).draw();

        } else {

        }
    }
}