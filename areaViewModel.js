//
// Model object:
// {
// 		properties:
// 		{
// 			tileType: 'grassland'
// 		}
// }
//
function initAreaViewModel(areaJq, model, coord) {
	areaJq.attr('id', coord);
	
	var detailsDiv = areaJq.find('.details');
	
	detailsDiv.append('<p>Tile type: ' + model.properties['tileType'] + '</p>');
}

function cleanUp(areaJq) {
	
}