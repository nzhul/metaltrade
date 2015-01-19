$(function() {

	// GOOGLE MAP
	$("#map-block").height($("#map-wrapper").height());	// Set Map Height
	function initialize($) {
		var mapOptions = {	
			zoom: 14,
			center: new google.maps.LatLng(42.7000, 23.3333),
			disableDefaultUI: true
		};
		var map = new google.maps.Map(document.getElementById('map-block'), mapOptions);
	}
	google.maps.event.addDomListener(window, 'load', initialize);

});	