$(document).ready(function () {
	const rocket_wrapper = $('.rocket-wrapper');
	const active_rocket = $('.active-rocket');
	const hover_rocket = $('.hover-rocket');
	const default_rocket = $('.default-rocket');
	let top = $(document).scrollTop();
	
	if (top != 0) {
		rocket_wrapper.css('opacity', 1);
	} 
	
	$(window).scroll(function () {
		let top = $(document).scrollTop();
		if (top == 0) {
			rocket_wrapper.css('opacity', 0);
			rocket_wrapper.hide();
			rocket_wrapper.css('transform', 'translate3d(0, 0, 0)');
			hover_rocket.show();
			default_rocket.show();
			active_rocket.css('opacity', 0);
		} else {
			rocket_wrapper.show();
			rocket_wrapper.css('opacity', 1);
		}
	})
	
	rocket_wrapper.on('click', function () {
		const screenHight = window.screen.height / 16;
		hover_rocket.hide();
		default_rocket.hide();
		active_rocket.css('opacity', '1');
		$(this).css('transform', 'translate3d(0, -'+ screenHight +'rem, 0)');
		$('html,body').animate({scrollTop: 0},1000);
	});
})