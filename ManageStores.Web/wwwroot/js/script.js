
// Get Current location
var x = document.getElementById('result');
function getCurrentLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            x.innerHTML = "Latitude, Longitude: " + position.coords.latitude + ",  " + position.coords.longitude;
        },
            function () {
                $scope.longitude = "57.396426";
                $scope.latitude = "14.673131";
            });
    }
    else {
        $scope.longitude = "57.396426";
        $scope.latitude = "14.673131";
    }
}

// Get store data
function displayStore() {
    var storeData = $("#selectedValue").val();
    $("p").html(storeData);
}
$("select").change(displayStore);
displayStore();


// Init Google Map
function initMap() {
    const map = new google.maps.Map(document.getElementById("map"), {
        zoom: 12,
        center: { lat: 56.83324, lng: 13.94082 },
    });
    const geocoder = new google.maps.Geocoder();
    const infowindow = new google.maps.InfoWindow();
    document.getElementById("submit").addEventListener("click", () => {
        geocodeLatLng(geocoder, map, infowindow);
    });
}

// Get pin marker in Google Map (using coordinates)
function geocodeLatLng(geocoder, map, infowindow) {
    const input = document.getElementById("latlng").value;
    const latlngStr = input.split(",", 2);
    const latlng = {
        lat: parseFloat(latlngStr[0]),
        lng: parseFloat(latlngStr[1]),
    };
    geocoder.geocode({ location: latlng }, (results, status) => {
        if (status === "OK") {
            if (results[0]) {
                map.setZoom(12);
                const marker = new google.maps.Marker({
                    position: latlng,
                    map: map
                });
                infowindow.setContent(results[0].formatted_address);
                infowindow.open(map, marker);
            } else {
                window.alert("No results found");
            }
        } else {
            window.alert("Geocoder failed due to: " + status);
        }
    });
}