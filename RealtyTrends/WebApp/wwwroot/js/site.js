$(document).ready(function() {
    // Event listeners for county, parish, and city selects
    $("#countySelect, #parishSelect, #citySelect").change(function() {
        let selectedRegionId = $(this).val();
        let childDropdowns = getChildDropdowns($(this));

        resetDropdowns(childDropdowns);

        if (selectedRegionId !== "") {
            let url = getPageUrl()
            
            fetchRegions(url, selectedRegionId, childDropdowns);
        }
    });

    function getPageUrl() {
        let currentUrl = window.location.href;
        let url;
        if (currentUrl.includes("PriceStatistics")) { 
            url = "/PriceStatistics/GetParentRegion/";
        } else{
            url = "/UserTriggers/GetParentRegion/";
        }
        return url;
    }
    
    // Event listener for start year select
    $("#startYear").change(function() {
        var selectedYear = $(this).val();
        resetDropdowns(["#startMonth", "#startDay"]);

        if (selectedYear !== "") {
            fetchMonths("/PriceStatistics/GetSelectedYearMonths", selectedYear, "#startMonth");
        }
    });

    // Event listener for start month select
    $("#startMonth").change(function() {
        let selectedYear = $("#startYear").val();
        let selectedMonth = $(this).val();
        resetDropdowns(["#startDay"]);

        if (selectedYear !== "" && selectedMonth !== "") {
            fetchDays("/PriceStatistics/GetSelectedMonthDays", selectedYear, selectedMonth, "#startDay");
        }
    });

    // Event listener for end year select
    $("#endYear").change(function() {
        var selectedYear = $(this).val();
        resetDropdowns(["#endMonth", "#endDay"]);

        if (selectedYear !== "") {
            fetchMonths("/PriceStatistics/GetSelectedYearMonths", selectedYear, "#endMonth");
        }
    });

    // Event listener for end month select
    $("#endMonth").change(function() {
        var selectedYear = $("#endYear").val();
        var selectedMonth = $(this).val();
        resetDropdowns(["#endDay"]);

        if (selectedYear !== "" && selectedMonth !== "") {
            fetchDays("/PriceStatistics/GetSelectedMonthDays", selectedYear, selectedMonth, "#endDay");
        }
    });
    

    $(".delete-user-trigger").submit(function(event) {
        event.preventDefault(); // Prevent the default form submission
        let el = event.currentTarget;
  
        let triggerId = $(el).data("trigger-id");
        
        $.ajax({
            url: "/UserTriggers/DeleteUserTrigger/" + triggerId,
            type: "POST",
            success: function() {

                $('#wrap-'+triggerId).remove();
            },
            error: function(xhr, status, error) {
                // Handle error response
                console.log(error);
            }
        });
    });

    function resetDropdowns(dropdowns) {
        $.each(dropdowns, function(index, dropdown) {
            var dropdownId = $(dropdown);
            dropdownId.empty().append('<option value=""></option>').prop("disabled", true);
        });
    }

    function getChildDropdowns(parentDropdown) {
        var childDropdowns = [];

        if (parentDropdown.attr("id") === "countySelect") {
            childDropdowns.push("#parishSelect", "#citySelect", "#streetSelect");
        } else if (parentDropdown.attr("id") === "parishSelect") {
            childDropdowns.push("#citySelect", "#streetSelect");
        } else if (parentDropdown.attr("id") === "citySelect") {
            childDropdowns.push("#streetSelect");
        }

        return childDropdowns;
    }

    function fetchRegions(url, parentId, dropdowns) {
        $.ajax({
            url: url + parentId,
            type: "GET",
            success: function(data) {
                populateDropdowns(data, dropdowns);
            },
            error: function(xhr, status, error) {
                console.log(error);
            }
        });
    }

    function fetchMonths(url, selectedYear, dropdownId) {
        $.ajax({
            url: url,
            type: "GET",
            data: { year: selectedYear },
            success: function(data) {
                populateDropdown(data, dropdownId);
            },
            error: function(xhr, status, error) {
                console.log(error);
            }
        });
    }

    function fetchDays(url, selectedYear, selectedMonth, dropdownId) {
        $.ajax({
            url: url,
            type: "GET",
            data: { year: selectedYear, month: selectedMonth },
            success: function(data) {
                populateDropdown(data, dropdownId);
            },
            error: function(xhr, status, error) {
                console.log(error);
            }
        });
    }

    function populateDropdowns(regions, dropdowns) {
        $.each(dropdowns, function(index, dropdown) {
            var dropdownId = $(dropdown);
            dropdownId.empty();

            if (typeof regions === "string") {
                regions = JSON.parse(regions);
            }
            
            dropdownId.append($('<option></option>').val('').text('Select'));
            $.each(regions, function(index, region) {
                dropdownId.append($('<option></option>').val(region.Id).text(region.Name));
            });

            dropdownId.prop("disabled", false);
        });
    }

    function populateDropdown(options, dropdownId) {
        let dropdown = $(dropdownId);
        dropdown.empty();

        dropdown.append($('<option></option>').val('').text('Select'));
        $.each(options, function(index, option) {
            dropdown.append($('<option></option>').val(option).text(option));
        });

        dropdown.prop("disabled", false);
    }

    function collectFilters(isTriggerButton) {
        
        let startDate;
        let endDate;
        
        if (!isTriggerButton) {
            let startYear = $("#startYear").val();
            let startMonth = $("#startMonth").val().padStart(2, '0');
            let startDay = $("#startDay").val().padStart(2, '0');
            startDate = startYear + "/" + startMonth + "/" + startDay;
            
            let endYear = $("#endYear").val();
            let endMonth = $("#endMonth").val().padStart(2, '0');
            let endDay = $("#endDay").val().padStart(2, '0');
            
            if (endYear === "") {
                endDate = getCurrentDate();
            } else {
                endDate = endYear + "/" + endMonth + "/" + endDay;
            }
        }
        
        let statisticFilters = {};

        statisticFilters.CountySelect = $("#countySelect").val() || null;
        statisticFilters.ParishSelect = $("#parishSelect").val() || null;
        statisticFilters.CitySelect = $("#citySelect").val() || null;
        statisticFilters.StreetSelect = $("#streetSelect").val() || null;
        statisticFilters.TransactionType = $("#transactionType").val() || null;
        statisticFilters.PropertyType = $("#propertyType").val() || null;
        statisticFilters.RoomsCountMin = $("#MinRoomsCount").val() || null;
        statisticFilters.RoomsCountMax = $("#MaxRoomsCount").val() || null;
        statisticFilters.AreaMin = $("#MinArea").val() || null;
        statisticFilters.AreaMax = $("#MaxArea").val() || null;
        statisticFilters.StartDate = startDate || getCurrentDate();
        statisticFilters.EndDate = endDate || getCurrentDate();

        if (isTriggerButton) {
            statisticFilters.TriggerPricePerUnit = $("#TriggerPricePerUnit").val() || null;
            statisticFilters.TriggerName = $("#TriggerName").val() || null;
        } else {
            statisticFilters.PricePerUnitMax = $("#MaxPricePerUnit").val() || null;
            statisticFilters.PricePerUnitMin = $("#MinPricePerUnit").val() || null;
        }

        return statisticFilters;
    }

    function sendData(statisticFilters, url) {
        // Send the AJAX request
        $.ajax({
            url: url,
            type: "POST",
            data: JSON.stringify(statisticFilters),
            contentType: "application/json",
            success: function(data) {
                if (url === "/PriceStatistics/GetPriceStatistics") {
                    updateGraph(data);
                }else {
                    location.reload();
                }
            },
            error: function(xhr, status, error) {
                console.log(error);
            }
        });
    }

    $("#filters-form").submit(function(e) {
        e.preventDefault();
        let statisticFilters = collectFilters(false);
        console.log(statisticFilters);
        sendData(statisticFilters, "/PriceStatistics/GetPriceStatistics");
    });

    $("#user-trigger-form").submit(function(e) {
        e.preventDefault();
        let statisticFilters = collectFilters(true);
        console.log(statisticFilters);
        sendData(statisticFilters, "/UserTriggers/NewUserTrigger");
    });
    
    function getCurrentDate() {
        const currentDate = new Date();
        const year = currentDate.getFullYear();
        const month = String(currentDate.getMonth() + 1).padStart(2, '0');
        const day = String(currentDate.getDate()).padStart(2, '0');
        const formattedDate = `${year}/${month}/${day}`;
        return formattedDate;
    }

    // Initialize the empty graph
    let svg, xScale, yScale, line, margin, graphWidth, graphHeight;

// Initialize the empty graph
    //if current page href contains PriceStatistics
    
    if (window.location.href.indexOf("PriceStatistics") > -1) {
        generateGraph();
    }
    function generateGraph() {
        const graphContainer = document.getElementById('graphContainer');
        let width = graphContainer.getBoundingClientRect().width;
        let height = graphContainer.getBoundingClientRect().height;
        margin = { top: 20, right: 30, bottom: 30, left: 60 };
        graphWidth = width - margin.left - margin.right;
        graphHeight = height - margin.top - margin.bottom;

        // Set up the scales
        xScale = d3.scaleTime().range([0, graphWidth]);
        yScale = d3.scaleLinear().range([graphHeight, 0]);

        // Set up the line generator function
        line = d3.line()
            .x(d => xScale(d.SnapshotDate))
            .y(d => yScale(d.AveragePricePerUnit));

        // Create the SVG element
        svg = d3.select("#graphContainer")
            .append("svg")
            .attr("width", width)
            .attr("height", height)
            .append("g")
            .attr("transform", `translate(${margin.left},${margin.top})`);

        // Add the x-axis
        svg.append("g")
            .attr("class", "x-axis")
            .attr("transform", `translate(0,${graphHeight})`);

        // Add the y-axis
        svg.append("g")
            .attr("class", "y-axis");

        // Add the x-axis with gridlines
        svg.append("g")
            .attr("class", "grid")
            .attr("color", "#9099ED")
            .attr("transform", "translate(0," + graphHeight + ")")
            .call(d3.axisBottom(xScale)
                .ticks(5)
                .tickSize(-graphHeight)
                .tickFormat("")
            );

        // Add the y-axis with gridlines
        svg.append("g")
            .attr("class", "grid")
            .attr("color", "#9099ED")
            .call(d3.axisLeft(yScale)
                .ticks(5)
                .tickSize(-graphWidth)
                .tickFormat("")
            );
    }

    function updateGraph(data) {
        // Parse the date values
        data = JSON.parse(data);
        
        const parseDate = d3.timeParse("%Y-%m-%d");
        data.forEach(d => {
            d.SnapshotDate = parseDate(d.SnapshotDate);
        });

        // Update the scales' domains based on the data
        xScale.domain(d3.extent(data, d => d.SnapshotDate));
        yScale.domain([0, d3.max(data, d => d.AveragePricePerUnit)]);

        // Select and update the x-axis
        svg.select(".x-axis")
            .call(d3.axisBottom(xScale));

        // Select and update the y-axis
        svg.select(".y-axis")
            .call(d3.axisLeft(yScale));

        // Select and update the line path
        const path = svg.select(".line");
        if (path.empty()) {
            svg.append("path")
                .datum(data)
                .attr("class", "line")
                .attr("fill", "none")
                .attr("stroke", "#9768D3")
                .attr("stroke-width", 2.5)
                .attr("d", line);
        } else {
            path.datum(data).attr("d", line);
        }

        // Add circles for data points
        const circles = svg.selectAll(".dot")
            .data(data);

        circles.enter()
            .append("circle")
            .attr("class", "dot")
            .attr("r", 8)
            .attr("cx", d => xScale(d.SnapshotDate))
            .attr("cy", d => yScale(d.AveragePricePerUnit))
            .attr("fill", "#9768D3");

        circles.exit().remove();
    }

    window.addEventListener('resize', () => {
        const graphContainer = document.getElementById('graphContainer');
        const newWidth = graphContainer.getBoundingClientRect().width;
        const newHeight = graphContainer.getBoundingClientRect().height;

        // Recalculate graphWidth and graphHeight, and update scales, axes, and other elements as needed
        graphWidth = newWidth - margin.left - margin.right;
        graphHeight = newHeight - margin.top - margin.bottom;

        xScale.range([0, graphWidth]);
        yScale.range([graphHeight, 0]);

        svg.select('.x-axis')
            .attr('transform', `translate(0,${graphHeight})`)
            .call(d3.axisBottom(xScale).tickFormat(d3.timeFormat("%Y-%m-%d")));

        svg.select('.y-axis')
            .call(d3.axisLeft(yScale));

        svg.select('.line')
            .attr('d', line);

        // update the grid lines
        svg.select(".grid")
            .attr("transform", `translate(0,${graphHeight})`)
            .call(make_x_gridlines(xScale)
                .tickSize(-graphHeight)
                .tickFormat(""));

        svg.select(".grid")
            .call(make_y_gridlines(yScale)
                .tickSize(-graphWidth)
                .tickFormat(""));
    });

    function make_x_gridlines(x) {
        return d3.axisBottom(x)
            .ticks(5)
    }

    function make_y_gridlines(y) {
        return d3.axisLeft(y)
            .ticks(5)
    }

});
