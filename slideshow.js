  // the slides - relative or complete urls for any number of swappable images
  var slides = new Array('XMTuner.png','XMTuner-WhatIs.png','XMTuner-Player.png','XMTuner-HistoryUI.png', 'XMTunerServiceControlV2.png', 'XMTuner-TVersity.png', 'XMTuner-WhatsOn.png');
  var slideIndex = 0; // which slide are we viewing?
  var fadeTimer = 1; // time, in seconds, it takes to swap images
  var swapTimer = 6; // time, in seconds, between image swaps
   
  var indexes = new Array();
  var timer;
   
  // gotta set it all up before we run it...
  function initSwap() {
	  // CAREFUL! we ASSUME there's a page element with id 'show' to
	  // contain the effected images
	  var show = document.getElementById('show');
	  var showindex = document.getElementById('show-index');
	  // set up the actual images
	  for(var i=0;i<slides.length;i++) {
		  // hooray for the DOM
		  var slide = document.createElement('img'); // a new image
		  slide.src = 'images/'+slides[i]; // should show this image
		  slide.style.position = 'absolute'; // important so images stay on top of each other
		  slide.style.left = '0px';
		  slide.style.opacity = '0'; // init to transparent (CSS2)
		  slide.style.filter = 'alpha(opacity:0)'; // init to transparent (MSIE)
		  show.appendChild(slide); // put the image in the box
		  slides[i] = slide; // reassign to same array for convenience
		  
		  var index = document.createElement('a');
		  var j = i+1;
		  index.innerHTML = '['+j+'] ';
		  index.href = 'javascript:goToSlide('+i+')';
		  //index.onmouseover = function(){goToSlide(i)}
		  //index.onmouseout = function(){resumeSwap()}
		  // index.style.color = 'white';
		  showindex.appendChild(index);
		  indexes[i] = index;
		  
		  // if it's the first image, let's show it now to avoid waiting
		  if(i==0) {
			  fade(i,1); // fade it in!
			  timer = setTimeout(doSwap,swapTimer*1000); // start the swap timer!
		  }
		  // repeat for each slide
	  }
  }
   
  function doSwap() {
	  var s1 = slideIndex; // where *are* we?
	  var s2 = s1+1==slides.length?0:s1+1; // either the next or the first
	  // we just wrapped to the beginning if we hit the end of the array, so...
	  slideIndex = s2; // update slide index
	  fade(s1,0); // fade the old slide out!
	  fade(s2,1); // fade the new slide in!
	  timer = setTimeout(doSwap,swapTimer*1000); // do it again in swapTimer seconds!
  }
   
  function fade(whoid,dir) {
	  var slide = slides[whoid]; // get the slide element at index whoid
	  var completed; // so we know when the fade is done
	  var opac = parseFloat(slide.style.opacity,10); // get a reference value
	  // increment if fading in, decrement if fading out
	  if(dir > 0) {
		  opac += .1; // more opaque
		  if(opac >= 1) {
			  // fade is at max! fade done!
			  completed = true;
		  }
	  } else {
		  opac -= .1; // less opaque
		  if(opac <= 0) {
			  // fade is at min! fade done!
			  completed = true;
		  }
	  }
	  slide.style.opacity = opac; // set opacity (CSS2)
	  slide.style.filter = 'alpha(opacity:'+(opac*100)+')'; // set opacity (MSIE)
	  if(!completed) {
		  // as long as the fade is not complete, keep doing this in 1/10 increments within fadeTimer seconds
		  setTimeout("fade("+whoid+","+dir+")",parseInt(fadeTimer/10,10));
	  }
  }
  
  function goToSlide(id) {
		fade(slideIndex, 0);
		slideIndex = id;
		fade(id, 1);
		clearTimeout(timer);
		timer = setTimeout(doSwap,30000); //Resume after a bit on our own.
  
  }
  
  // start it up when the page loads!
  // ideally you want to place this in an onload appender or manager - if you use
  // a lot of JS you may end up overriding your onload
  onload = initSwap;
