
// owl carousel js.
$('.owl-carousel').owlCarousel({
    loop: true,
    margin: 10,
    nav: false,
    dots: true,
    autoplay: false,
    autoplayHoverPause: true,
    responsive: {
        0: {
            items: 1
        },
        600: {
            items: 2
        },
        1000: {
            items: 4
        }
    }
});


//var ctx = document.getElementById("myChart08").getContext('2d');


//var myChart = new Chart(ctx, {
//    type: 'line',
//    data: {
//        labels: ["Tokyo", "Mumbai", "Mexico City", "Shanghai", "Sao Paulo", "New York", "Karachi", "Buenos Aires", "Delhi", "Moscow"],
//        datasets: [{
//            label: 'Series 08', // Name the series
//            data: [10, 20, 30, 40, 50, 60, 70, 80, 90, 100, ], // Specify the data values array
//            fill: false,
//            borderColor: '#2196f3', // Add custom color border (Line)
//            backgroundColor: '#2196f3', // Add custom color background (Points and Fill)
//            borderWidth: 1 // Specify bar border width
//        }]
//    },
//    options: {
//        responsive: true, // Instruct chart js to respond nicely.
//        maintainAspectRatio: false, // Add to prevent default behaviour of full-width/height 
//    }
//});


// count-maxlength js.
