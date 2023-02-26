var map;

function iniciarMap(){
    var coord = {lat:-34.5956145 ,lng: -58.4431949};
    map = new google.maps.Map(document.getElementById('map'),{
      zoom: 10,
      center: coord
    });
    var marker = new google.maps.Marker({
      position: coord,
      map: map
    });
    loadRoute();
}

function loadPosition(coord){
      map = new google.maps.Map(document.getElementById('map'),{
      zoom: 20,
      center: coord
    });
}


function loadRoute(){
