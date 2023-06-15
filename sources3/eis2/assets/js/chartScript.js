$(document).ready(function () {
  lineChart();
  barChart();
  pieChart()
})


function lineChart(){
  var bulan = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
  var config = {
      type: 'line',
      data: {
          labels: bulan,
          datasets: [{
              label: "My First dataset",
              backgroundColor: window.chartColors.red,
              borderColor: window.chartColors.red,
              data: [12, 19, 3, 5, 2, 3],
              fill: false,
          }, {
              label: "My Second dataset",
              fill: false,
              backgroundColor: window.chartColors.blue,
              borderColor: window.chartColors.blue,
              data:[14, 11, 13, 5, 5, 1],
          }]
      },
      options: {
          responsive: true,
          title:{
              display:true,
              text:'Chart.js Line Chart'
          },
          tooltips: {
              mode: 'index',
              intersect: false,
          },
          hover: {
              mode: 'nearest',
              intersect: true
          },
          scales: {
              xAxes: [{
                  display: true,
                  scaleLabel: {
                      display: true,
                      labelString: 'Months'
                  }
              }],
              yAxes: [{
                  display: true,
                  scaleLabel: {
                      display: true,
                      labelString: 'Values'
                  }
              }]
          }
      }
  };
  var ctx = document.getElementById("canvas").getContext("2d");
  window.myLine = new Chart(ctx, config);
}

function barChart(){
  var MONTHS = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
  var color = Chart.helpers.color;
  var barChartData = {
      labels: ["January", "February", "March", "April", "May", "June", "July"],
      datasets: [{
          label: 'Dataset 1',
          backgroundColor: color(window.chartColors.red).alpha(0.5).rgbString(),
          borderColor: window.chartColors.red,
          borderWidth: 1,
          data: [12,15,3,11,12,6]
      }, {
          label: 'Dataset 2',
          backgroundColor: color(window.chartColors.blue).alpha(0.5).rgbString(),
          borderColor: window.chartColors.blue,
          borderWidth: 1,
          data: [15,1,8,1,14,8]
      }]

  };

  var ctx = document.getElementById("coloumn").getContext("2d");
  window.myBar = new Chart(ctx, {
      type: 'bar',
      data: barChartData,
      options: {
          responsive: true,
          legend: {
              position: 'top',
          },
          title: {
              display: true,
              text: 'Chart.js Bar Chart'
          }
      }
  });

}

function pieChart(){
  var randomScalingFactor = function() {
      return Math.round(Math.random() * 100);
  };

  var config = {
      type: 'pie',
      data: {
          datasets: [{
              data: [12,12,15,16,22],
              backgroundColor: [
                  window.chartColors.red,
                  window.chartColors.orange,
                  window.chartColors.yellow,
                  window.chartColors.green,
                  window.chartColors.blue,
              ],
              label: 'Dataset 1'
          }],
          labels: [
              "Red",
              "Orange",
              "Yellow",
              "Green",
              "Blue"
          ]
      },
      options: {
          responsive: true
      }
  };

  var ctx = document.getElementById("pie").getContext("2d");
  window.myPie = new Chart(ctx, config);
}
