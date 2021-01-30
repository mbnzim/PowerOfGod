var calApp;
calApp = angular.module('calApp', ['ngAnimate'])
calApp.controller('calCtrl',  function($scope, $sce) {
  
  $scope.eventsVisible = 3; // How many events should be shown?
  
  $scope.events = [
    {
      title    : "Youth Conference",
          date: 1791786245352,
          location: "21May St, Greville,Berea",
    },
    {
        title: "Crusade",    
        date: 1792786245352,
        location: "Ark Fellowship Centre - Zwelinja Sibia RdUmlazi FeThekwini4066",
    },
    {
        title: "Care Day",
        date: 1793786245352,
      location : "21May St, Greville,Berea",
    },
    {
      title    : "Family Day",
        date: 1783786245352,
      location : "Collins St, Melbourne",
    },
    {
      title    : "We Connect",
      date     : 1461990642447,
      location : "Altona Beach",
    }
  ];
  
  $scope.getMap = function(event){
    // Creates a Google Map URL
    return "https://maps.google.com/?q=" + event.location;
  };
    
 $scope.getEvents = function(){
  // Gets x number of events , using scope.eventsVisible 
  // to determine how many events to show
   var events = [];
   for(x = 0; x<$scope.eventsVisible; x++){
     events.push($scope.events[x]);
   }
   return events;
 } 
 
 $scope.hiddenEvents = function(){
   // Calculates how many events are hidden
   var remaining;
   if($scope.events.length - $scope.eventsVisible > 0){
     remaining = $scope.events.length - $scope.eventsVisible;
   } else{
     remaining = 0;
   }
   
   return remaining;
 }
 
 $scope.showHidden = function(){
   // Show hidden events
   $scope.eventsVisible = $scope.events.length;
 }
 $scope.hideEvents = function(){
   // Hide events
   $scope.eventsVisible = 3;
 } 
 
  
});