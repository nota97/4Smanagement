
function qp_doughnut_pie_chart(chartID, chartType, maxWidth){

		// chartType accepts values: 'doughnut' or 'pie'

		if($(chartID).length){
			var chartSizes = qp_chart_sizes(chartID);
			var chartWidth = chartSizes[0];
			var chartHeight = chartSizes[1];

			if(typeof(chartType) === 'undefined'){
				chartType = 'doughnut';
			}

			if(typeof(maxWidth) === 'undefined'){
				maxWidth = chartWidth;
			}
			if(maxWidth != chartWidth){
				chartWidth = maxWidth;
			}

			// This makes sure that the canvas always uses the size of the smaller value (width or height)
			if(chartWidth <= chartHeight){
				chartHeight = chartWidth;
			}else{
				chartWidth = chartHeight;
			}

			// Make width 80% of original size
			chartWidth = chartWidth * 0.8;
			chartHeight = chartHeight * 0.8;

			// If there is a date/range dropdown, then enable a click event
			// If not, use another trigger
			var clickedElement = $(chartID).closest('.card').find('.header-btn-block .data-range.dropdown .dropdown-item');
			var triggeredEvent = 'click';

			if(!clickedElement.length){
				var clickedElement = $(chartID);
				var triggeredEvent = 'load';
				// B5B Documentation:
				// Set the default range if no data/range dropdown is present
				var range = 'year';
			}

			clickedElement.on(triggeredEvent, function(e){
				e.preventDefault();

				// If default range is not set, then get the range from the clicked element
				if(triggeredEvent != "load"){
					var range = $(this).attr('href');
				}

				// Highlight clicked item as active
				$(this).siblings().removeClass('active');
				$(this).addClass('active');

				/* DEMO DATA - START */
				switch(range){
					case 'today':
					// B5B Documentation:
					// Use Ajax to pull your own data from the database
					var dataSet = [21, 21, 17, 5, 6];

					// Load the chart after all the data has been set
					load_chart(range, dataSet);
					break;

					case 'week':
					// B5B Documentation:
					// Use Ajax to pull your own data from the database
					var dataSet = [75, 34, 33, 63, 38];

					// Load the chart after all the data has been set
					load_chart(range, dataSet);
					break;

					case 'month':
					// B5B Documentation:
					// Use Ajax to pull your own data from the database
					var dataSet = [398, 925, 241, 127, 463];

					// Load the chart after all the data has been set
					load_chart(range, dataSet);
					break;

					default:
					case 'year':
					// B5B Documentation:
					// Use Ajax to pull your own data from the database
					var dataSet = [2241, 1217, 5525, 4363, 3998];

					// Load the chart after all the data has been set
					load_chart(range, dataSet);
					break;
				}
				/* DEMO DATA - END */
			});

			if(triggeredEvent == 'load'){
				clickedElement.trigger(triggeredEvent);
			}else{
				$(chartID).closest('.card').find('.header-btn-block .data-range.dropdown .dropdown-item.active').trigger(triggeredEvent);
			}

			function load_chart(range, dataSet){

				if(chartType == 'doughnut'){
					var cutoutPercentage = 88;
				}else if(chartType == 'polarArea'){
					var cutoutPercentage = 0;
					var areaOpacity = 0.7;
				}else{
					var cutoutPercentage = 0;
				}

				var canvasParent = $(chartID).closest('.card-chart');
				var color1 = qp_hexToRgbA(canvasParent.data('chart-color-1'), areaOpacity);
				var color2 = qp_hexToRgbA(canvasParent.data('chart-color-2'), areaOpacity);
				var color3 = qp_hexToRgbA(canvasParent.data('chart-color-3'), areaOpacity);
				var color4 = qp_hexToRgbA(canvasParent.data('chart-color-4'), areaOpacity);
				var color5 = qp_hexToRgbA(canvasParent.data('chart-color-5'), areaOpacity);

				// Note: Because chartjs 'responsive' option is set, then we have to set the size of the parent container to the chartWidth & chartHeight, since chartjs fills the entire parent container when this option is used
				canvasParent.css({'width' : chartWidth + 'px', 'height' : chartHeight + 'px'});

				// First remove old chart, then create new one
				canvasParent.empty();

				$('<canvas>').attr({
					id: chartID.substring(1),
					width: chartWidth + 'px',
					height: chartHeight + 'px'
				}).appendTo(canvasParent);

				var ctx = $(chartID);

				var myDoughnutChart = new Chart(ctx, {
					type: chartType,
					data: {
						datasets: [{
							data: dataSet,
							backgroundColor: [color1, color2, color3, color4, color5],
							label: 'Traffic Sources'
						}],
						labels: ["Google - Organic", "Google - Paid", "Facebook", "Twitter", "LinkedIn"]
					},
					options: {
						cutoutPercentage: cutoutPercentage,
						responsive: true,
						legend: {
							display: false
						},
						title: {
							display: false
						}
					}
				});
			}
		}
	}
qp_doughnut_pie_chart('#demo-pie-chart', 'pie');
