//
// Disable jQueried element and set the html
//
function disable(txt, jq) {
	jq.html(txt);
	jq.prop('disabled', true);
	
}

//
// Ensable jQueried element and set the html
//
function enable(txt, jq) {
	jq.html(txt);
	jq.prop('disabled', false);
}

function resetKO(jq) {
	ko.cleanNode(jq);
	jq.unbind();
}