/*
 * Solitaire Web Edition - The last hold out, Now we can convert our OS to the web browser
 * by Cory Rauch (cory@sysbotz.com)
 *
 * This software is freeware and has no warranty of any kind. Use at your own risk
 * Card images where used from kpoker - a kde project. www.kde.org
 *
 */
 
 /* In game variables */
 var card_size_w=72;
 var card_size_h=96;

 /* Holds cards positions */
 var cards=new Array();
 
 /* Holds current position in each slot */
 var slots=new Array(0,0,0,0,0,0,0,0,0,0,0,0);
 
 /* Mouse Stuff */
 var current_mousex=0;
 var current_mousey=0;
 var last_mousex=0;
 var last_mousey=0;
 var card_focus=new Array();
 var deck_draw_pos=0;
 
 /* Setup game in memory
  */
 function initGame() {
 	/* Clear Deck Array
	*/
	for(var c=0; c < 52; c++) {
		cards[c]=new Array();
		cards[c]["pos"]=0;
		cards[c]["slot"]=0;
		cards[c]["use"]=0;
		cards[c]["hi"]=0;
	}
	
	/* Fill slots with random cards and draw */
	var limit=1;
	var x=0;
	for(var c=1; c < 8; c++) {
		for(var i=0; i < limit; i++) {
			x=randomCard(c);
			if(i == (limit - 1)) {
				drawCard(x, 1);
			} else {
				drawCard(x, 0);
			}
		}
		limit++;
	}
	
	/* Draw Home Spots */
	for(var c=8; c < 12; c++) {
		child=document.createElement('IMG');
		child.style.position="absolute";
		child.src="images/deck-empty.jpg";
		child.style.top="5px";
		child.style.left=((c - 5) * (card_size_w + 25))+"px";
		child.style.zIndex=0;
		document.body.appendChild(child);
	}
	
	/* Fill deck randomly */
	for(var c=0; c < 24; c++)
		randomCard(0);

	/* Draw Deck */
	child=document.createElement('DIV');
	child.id="deckdiv";
	child.style.position="absolute";
	child.style.top="5px";
	child.style.left="5px";
	child.style.zIndex=0;
	child.innerHTML="<A HREF=\"javascript:deckNext();\"><IMG SRC=\"images/deck.jpg\" border=\"0\"></A>";
	document.body.appendChild(child);
	
	slots[0] = slotEnd(0);
	
	/* Mouse Event Handlers */
	window.document.onmousedown=event_mousedown;
	window.document.onmouseup=event_mouseup;
	
    // prevent IE text selection while dragging!!!
    // Little-known trick!
    document.body.ondrag = function () { return false; };
    document.body.onselectstart = function () { return false; };	
 }
 
 /* Grabs a random free card */
 function randomCard(p) {
 	while((n = Math.round(Math.random()*51)) > 52);
	
	if(cards[n]['use'] == 0 && (cards[n])) {
		cards[n]['use']=1;
		cards[n]['slot']=p;
		slots[p]+=1;
		cards[n]['pos']=slots[p];
	} else {
		return randomCard(p);
	}
	
 	return n;
 }
 
 /* Draw Card as specified in cards array, second parameters controls if card
  * is shown (1) or hidden (0).
  */
 function drawCard(n, hi) {
	if(! cards[n])
		return;
		
	cards[n]['hi'] = hi;
	/* Delete current drawn card */
 	if(document.getElementById("card"+n))
		document.body.removeChild(document.getElementById("card"+n));
	
	/* Create Card */
	child=document.createElement('IMG');
	child.id="card"+n;
	child.style.position="absolute";
	
	if(hi == 1) {
		child.src="images/"+(n + 1)+".jpg";
	} else {
		child.src="images/deck.jpg";
	}
	
	pos=cardPos(n);
	child.style.top=pos[0]+"px";
	child.style.left=pos[1]+"px";

	child.style.zIndex=cards[n]['pos']+1;
	document.body.appendChild(child);
 }
 
 /* Calculate cards position */
 function cardPos(n) {
 	var t=0;
	var l=0;
	switch(cards[n]['slot']) {
		case 0:
		t=5;
		l=((card_size_w + 25) + (deck_draw_pos * 15));
/*		if(deck_draw_pos > 2) {
			deck_draw_pos=0;
		} else {
			deck_draw_pos++;
		} */
		break;
		
		case 8:
		case 9:
		case 10:
		case 11:
		t=5;
		l=((cards[n]['slot'] - 5) * (card_size_w + 25));
		break;
		
		default:
		t=((card_size_h + 30) + (cards[n]['pos'] * 15));
		l=(((cards[n]['slot'] - 1) * (card_size_w + 25)) + 5);
	}
	
	return new Array(t, l);
 }
 
 /* Calculates the position of a slot */
 function slotLoc(s) {
 	var t=0;
	var l=0;
	var h=card_size_h;
	var w=card_size_w;
	switch(s) {
		case 8:
		case 9:
		case 10:
		case 11:
		t=5;
		l=((s - 5) * (card_size_w + 25));
		w += l;
		h += t;
		break;
		
		default:
		t=(card_size_h + 35);
		l=(((s - 1) * (card_size_w + 25)) + 5);
		w += l;
		h += t + 700;
	}
	
	return new Array(t, l, h, w);
 }
 
 /* Delete Drawn Cards */
 function deleteCard(n) {
 	if(cards[n])
		cards[n]['hi'] = 0;

	if(document.getElementById("card"+n))
		document.body.removeChild(document.getElementById("card"+n));
 }
 
 /* Grab next 3 cards off deck
 */
 function deckNext() {
 /* Delete Drawn Cards */
 	deleteCard(slotPos(0, slots[0]));
/*	deleteCard(slotPos(0, slots[0]+1)); */
/*	deleteCard(slotPos(0, slots[0]+2)); */

 /* advance slot position */
/* 	for(var i=0; i < 3; i++) { */
	 	if(slots[0] >= slotEnd(0)) {
			slots[0]=1;
		} else {
			slots[0]++;
		}
	
	if(slots[0] >= slotEnd(0)) {
		document.getElementById("deckdiv").innerHTML="<A HREF=\"javascript:deckNext();\"><IMG SRC=\"images/deck-empty.jpg\" border=\"0\"></A>";	
	} else {
		document.getElementById("deckdiv").innerHTML="<A HREF=\"javascript:deckNext();\"><IMG SRC=\"images/deck.jpg\" border=\"0\"></A>";	
	}
	
	if(slots[0] <= slotEnd(0))
		while((p=slotPos(0, slots[0])) == 53)
			slots[0]++;

	drawCard(slotPos(0, slots[0]), 1);
/*	} */
}
 
 /* Searches for end of a slot */
 function slotEnd(s) {
 	var n = 0;
 	for(var c=0; c < 52; c++)
		if(cards[c]['slot'] == s && n < cards[c]['pos'])
			n = cards[c]['pos'];
	
	return n;
 }
 
 /* Searches to find the end card */
 function endCard(s) {
 	for(var c=0; c < 52; c++)
		if(cards[c]['slot'] == s && cards[c]['pos'] == slots[s])
			return c;
	
	return 53;
 }
  
 /* Searches for card at slot s and position p */
 function slotPos(s, p) {
 	for(var c=0; c < 52; c++) {
		if(cards[c]['slot'] == s && cards[c]['pos'] == p)
			return c;
	}
	
	return 53;
 }
 
 function unselectText() {
	if(document.selection)
		if(document.selection.empty)
			document.selection.empty();
	
//	if(window.find)
	//	window.find(' ');
 }
 
 /*
 Mouse Up Event Function
*/
 function event_mouseup(e) {
 	if(card_focus.length == 0)
		return;
		
 	e=e || window.event;
    current_mousex=getMouseX(e);
	current_mousey=getMouseY(e);
	document.onmousemove=null;

// Unselect any selected text
	unselectText();
	
	/* Determine what slot if any slot user has dragged too */
	
	/* Check which Slot */
	var mode = 1;
	for(var c=1; c < 12; c++) {
		pos=slotLoc(c);
		switch(c) {
		/* Home Spots */
			case 8:
			case 9:
			case 10:
			case 11:
			mode = 0;
			break;
		
		/* Normal Slots */
			default:
			mode = 1;
		}
		
		if(current_mousey >= pos[0] && current_mousey <= pos[2] && current_mousex >= pos[1] && current_mousex <= pos[3])
			if(legalMove(card_focus[0] + 1, c, mode)) {
				for(var i=0; i < card_focus.length; i++) {
					moveCard(card_focus[i], c);
					drawCard(card_focus[i], 1);
				}
				card_focus=new Array();
				
				/* Check if the user won */
				didTheyWin();			
				return;
			}
	}

/*	if(card_focus[0]['slot'] == 0) {
		drawCard(slotPos(0, slots[0]), 1);
		drawCard(slotPos(0, slots[0]+1), 1);
		drawCard(slotPos(0, slots[0]+2), 1);
	} else { */
		for(var i=0; i < card_focus.length; i++)
			drawCard(card_focus[i], 1);
//	}

	card_focus=new Array();
 }
 
/* Check If They Won and then displays a message if they did */
 function didTheyWin() {
 	var s8=endCard(8) + 1;
	var s9=endCard(9) + 1;
	var s10=endCard(10) + 1;
	var s11=endCard(11) + 1;

	if(s8 > 4 && s8 < 9 && s9 > 4 && s9 < 9 && s10 > 4 && s10 < 9 && s11 > 4 && s11 < 9) {
	/* You Won */
		child=document.createElement('DIV');
		child.id="youwin";
		child.style.position="absolute";
		child.style.top=(screen.height / 2) - 100;
		child.style.left=(screen.width / 2) - 100;
		child.innerHTML="<h3>You Win!</h3><br>Hit Refresh To Restart<br>Or visit us at <A HREF=http://www.sysbotz.com>Sysbotz.com</A>";
		document.body.appendChild(child);
	}
 }
 
/* Do the Move */
 function moveCard(n, c) {
	slots[cards[n]['slot']]--;
	cards[n]['slot']=c;
	cards[n]['pos']=slots[c] + 1;
	slots[c]++;
 }
 
/* Checks if move is legal, mode=0 for home spots and mode=1 for spots mode */
 function legalMove(v, s, mode) {
	/* Check if its an empty home spot, then the only valid card is an ace */
	if(endCard(s) == 53 && s > 7) {
		if(v > 4) {
			return false;
		} else {
			return true;
		}
	} else {
		/* If empty normal slot then the only valid card is a king */
		if(endCard(s) == 53 && s < 8) {
			if(v == 5 || v == 6 || v == 7 || v == 8) {
				return true;
			} else {
				return false;
			}
		} else {
	 	/* First determine if black or red */
			var color1 = cardColor(v);
	
		/* Then determine if target card is black or red */
			var color2 = cardColor((endCard(s) + 1));

		/* See if color ok */
			if((color1 == color2 && s < 8 && s > 0) || (color1 != color2 && s > 7))
				return false;
	
		/* The one exception to the rule the ace on the two */
			if(s < 8 && s > 0) {
				if(v < 5 && (endCard(s) + 1) > 48) {
					return true;
				} else {
					if(v < (endCard(s) + 1))
						return false;
					
					var t1=v % 4;
					var t2=(endCard(s) + 1);
					switch(t1) {
						case 1:
						if(t2 != (v-1) && t2 != (v-2))
							return false
						break;
						
						case 2:
						if(t2 != (v-2) && t2 != (v-3))
							return false
						break;
						
						case 3:
						if(t2 != (v-5) && t2 != (v-6))
							return false
						break;
						
						case 0:
						if(t2 != (v-6) && t2 != (v-7))
							return false
						break;
					}
					
					return true;
				}
			} else {
				var t1=v % 4;
				var t2=(endCard(s) + 1) % 4;

				if(t1 != t2)
					return false;
				
		/* oh ya the other exception to the rule the two on the ace in the home spots */
				if(v >= 49 && (endCard(s) + 1) <= 4) {
					return true;
				} else {
					if((endCard(s) + 1) != (v+4))
						return false;
					return true;
				}
			}
		}
	}
	
	return false;
 }
 
 /* Determine Card Color */
 function cardColor(n) {
	var t = n % 4;
	if(t == 1 || t == 2) {
		var color = "black";
	} else {
		var color = "red";
	}
	
	return color;
 }
 
/*
 Mouse Down Event Function
*/
 function event_mousedown(e) {
 	if(card_focus.length != 0)
		return;
	
 // Call widgets mouse down event
 	e=e || window.event;
    mx=current_mousex=getMouseX(e);
	my=current_mousey=getMouseY(e);

// Unselect any selected text
	unselectText();

	var w=0;
	var h=0;
 	for(var c=0; c < 52; c++) {
		pos=cardPos(c);
		
/* Calculates amount of surface showing if any */
	switch(cards[c]['slot']) {
		case 0:
		h=card_size_h;
		if(endCard(cards[c]['slot']) == c) {
			w=card_size_w;
		} else {
			w=0;
		}
		break;
				
		case 8:
		case 9:
		case 10:
		case 11:
		if(endCard(cards[c]['slot']) == c) {
			w=card_size_w;
			h=card_size_h;
		} else {
			w=0;
			h=0;
		}
		break;
				
		default:
		w=card_size_w;
		if(endCard(cards[c]['slot']) == c) {
			h=card_size_h;
		} else {
			h=15;
		}
	}
				
		if(document.getElementById("card"+c)) {
			if(mx >= pos[1] && mx <= (pos[1] + w) && my >= pos[0] && my <= (pos[0] + h) && (cards[c]['hi'] != 0 || endCard(cards[c]['slot']) == c)) {
			// allow user to move selected card
			/* Select all cards on top of selected card */
				var limit = cards[endCard(cards[c]['slot'])]['pos'];
				limit++;
				for(var x=cards[c]['pos']; x < limit; x++) {
					var t=slotPos(cards[c]['slot'], x);
					if(cards[t]) {
						card_focus.push(t);
						document.getElementById("card"+t).style.zIndex=100 + cards[t]['pos'];
					}
				}
				
				document.onmousemove=event_mousemove;
				return;
			}
		}
	}
 }

 /*
 Moves a card
 */
 function event_mousemove(e) {
 	if(card_focus.length == 0)
		return;
	
 	e=e || window.event;
    last_mousex=current_mousex;
    last_mousey=current_mousey;
    current_mousex=getMouseX(e);
	current_mousey=getMouseY(e);

	unselectText();
	
	// move cards
	for(var i=0; i < card_focus.length; i++) {
		elm = document.getElementById("card"+card_focus[i]);
		if(elm != null) {
			elm.style.left = (parseInt(elm.style.left) + (current_mousex - last_mousex))+"px";
			elm.style.top = (parseInt(elm.style.top) + (current_mousey - last_mousey))+"px";
		}
	}
 }
 
 /* Get X position of mouse */
 function getMouseX(e) {
	if(getBrowser() != 3) {
		return e.pageX;
	} else {
		return e.clientX;	
	}
 }

/* Get Y position of mouse */
 function getMouseY(e) {
	if(getBrowser() != 3) {
		return e.pageY;
	} else {
		return e.clientY;	
	}
 }
 
 /*
Get Browser
*/
 function getBrowser() {
 // Detects Browser

	var browserName=navigator.appName; 
	switch (browserName) {
		case "Netscape" :
		return 1;
		break;

		case "Microsoft Internet Explorer" :
		return 3;
		break;
		
		default :
		return 2;
		break;
	}
 }