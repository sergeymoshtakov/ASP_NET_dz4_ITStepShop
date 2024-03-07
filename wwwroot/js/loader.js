var showLoader = function (form) {
	$("<div />").css({
		'position': 'fixed',
		'left': 0,
		'right': 0,
		'bottom': 0,
		'top': 0,
		'background': '#0020ff36',
		'z-index': '99',
		display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
		'text-align': 'center'
	}).appendTo($("body"))
		.append(
			$("<img />").attr("src", "/images/loader.gif")
		);
}