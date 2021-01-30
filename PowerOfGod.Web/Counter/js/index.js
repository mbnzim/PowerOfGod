$(window).resize(function(){
    			$("#DateCountdown").TimeCircles().rebuild();
			});
			$("#DateCountdown").TimeCircles({
				"animation": "smooth",
				"bg_width": 0.5,
				"fg_width": 0.023333333333333334,
				"circle_bg_color": "#60686F",
					"time": {
						"Days": {
							"text": "Days",
							"color": "#FFCC66",
							"show": true
						},
        				"Hours": {
							"text": "Hours",
							"color": "#99CCFF",
							"show": true
						},
						"Minutes": {
							"text": "Minutes",
							"color": "#BBFFBB",
							"show": true
						},
						"Seconds": {
							"text": "Seconds",
							"color": "#FF9999",
							 "show": true
						}
					}
			});