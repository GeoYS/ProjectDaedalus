var gameViewModel = {};

function initGameViewModel() {
	gameViewModel.tilesWide = ko.observable(0);
	gameViewModel.tilesHigh = ko.observable(0);
	
	gameViewModel.dimensions = ko.computed(function() {
		return {
			tilesWide: gameViewModel.tilesWide(),
			tilesHigh: gameViewModel.tilesHigh()
		}
	});
	
	gameViewModel.dimensions.subscribe(mapUpdater);
	
	function mapUpdater(dimensions) {	
		var tilesHigh = dimensions['tilesHigh'];
		var tilesWide = dimensions['tilesWide'];
		var mapDiv = $('#map');
		mapDiv.empty();
		
		for(var y = 0; y < tilesHigh; y ++) {
			var row = 'row' + y;
			mapDiv.append('<div id=\'' + row + '\'></div>');
			$('#' + row).width(tilesWide * 100);
			
			for(var x = 0; x < tilesWide; x ++) {
				var coord = x + ',' + y;
				$('#' + row)
					.append(
						'<div class=\'tile ' + gameViewModel.tileMap[coord].properties['tileType'] + '\' ' + 'id=\'' + coord + '\'></div>');
			}
		}
	}
	
	database.ref('game/world/map').on('value', function(snapshot) {
		var map = snapshot.val();
		
		if(!snapshot.val()) {
			return;
		}
		
		var tilesWide = map['tilesWide'];
		var tilesHigh = map['tilesHigh'];
		gameViewModel.tileMap = {};
		
		for(var y = 0; y < tilesHigh; y ++) {			
			for(var x = 0; x < tilesWide; x ++) {
				var coord = x + ',' + y;
				var tile = {};
				tile.properties = {};
				tile.properties['tileType'] = map['tiles'][coord]['properties']['tileType'];
				tile.properties['areaId'] = map['tiles'][coord]['properties']['areaId'];
				gameViewModel.tileMap[coord] = tile;
			}
		}
		
		gameViewModel.tilesWide(tilesWide);
		gameViewModel.tilesHigh(tilesHigh);
	});
	
	database.ref('game/world/map').on('child_changed', function(snapshot) {
		var map = snapshot.val();
		
		if(!snapshot.val()) {
			return;
		}
		
		var tilesWide = map['tilesWide'];
		var tilesHigh = map['tilesHigh'];
		gameViewModel.tileMap = {};
		
		for(var y = 0; y < tilesHigh; y ++) {			
			for(var x = 0; x < tilesWide; x ++) {
				var coord = x + ',' + y;
				var tile = {};
				tile.properties = {};
				tile.properties['tileType'] = map['tiles'][coord]['properties']['tileType'];
				tile.properties['areaId'] = map['tiles'][coord]['properties']['areaId'];
				gameViewModel.tileMap[coord] = tile;
			}
		}
		
		gameViewModel.tilesWide(tilesWide);
		gameViewModel.tilesHigh(tilesHigh);
	});
}