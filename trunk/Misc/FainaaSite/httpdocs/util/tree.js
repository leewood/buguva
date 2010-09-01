function getTrees() {
	var treeObjs = $$('li.pagenav');
	treeObj = treeObjs[0];
	treeObj.onclick = treeAction;
	// percorre todos os filhos de #news atraves do array childNodes[]
	for (j=0;j<treeObj.childNodes.length;j++)
		if (treeObj.childNodes[j].tagName == 'UL') {
			// se for <ul>, chama ul2tree()
			ul2tree(treeObj.childNodes[j]);
		}
}

function ul2tree(ulObj) {
	var liObj;
	var liCollapsed;
	// percorre todos os filhos da <ul> atraves do array childNodes[]
	for (var i=0;i<ulObj.childNodes.length;i++)
		// tag filha = <li> ?
		if (ulObj.childNodes[i].tagName == 'LI') {
			liObj = ulObj.childNodes[i];
			// recupera todas as <ul> internas ao <li>
			var ulObjs = liObj.getElementsByTagName('UL');
			// ha algum <ul>?
			if (ulObjs.length) {
				liCollapsed = (liObj.className != 'expanded');
				// define a classe do <li> como 'folder'
				liObj.className = 'folder';
				// insere um link para expandir/fechar a sub-arvore do <li>
				liObj.insertBefore(newExpandLink('[*]',liCollapsed),liObj.firstChild);
				// se o item <li> estiver fechado, esconde o <ul>
				if (liCollapsed) ulObjs[0].style.display = 'none';
				// processa recursivamente a <ul> interna
				ul2tree(ulObjs[0]);
			}
		}
	// define a classe do ultimo <li> como 'last'
	if (liObj.className == '') liObj.className = 'last'
	else liObj.className += ' last';
}

function newSpan(text) {
	// cria um elemento <span>...</span>
	var span = document.createElement('SPAN');
	// cria um texto a partir do argumento 'text'
	var spanText = document.createTextNode(text);
	// insere o texto no <span>
	span.appendChild(spanText);
	return span;
}

function newExpandLink(text,collapsed) {
	// cria um elemento <a>...</a>
	var expandLink = document.createElement('A');
	// cria um <span>text</span> e o insere no <a>
	expandLink.appendChild(newSpan(text));
	// define a classe do <a> como 'minus' ou 'plus'
	if (collapsed) expandLink.className='plus' 
	else expandLink.className='minus';
	return expandLink;
}

function treeAction(obj) {
	// recupera o elemento clicado a partir do objeto que disparou o evento
	if (document.all) obj = event.srcElement // IE
	else obj = obj.target;
	// se o elemento nao for <a>, retorna
	if (obj.tagName == 'SPAN') obj = obj.parentNode;
	if (obj.tagName != 'A') return;
	// troca a classe
	if (obj.className == 'plus') obj.className = 'minus';
	else if (obj.className == 'minus')  obj.className = 'plus';
	else return;
	// pega a <li> associada
	var liObj = obj.parentNode;
	var ulObjs = liObj.getElementsByTagName('UL');
	// se nao houver <ul> interna, retorna
	if (!ulObjs.length) return;
	// esconde/mostra a <ul> interna
	if ( ulObjs[0].style.display == 'none' ) ulObjs[0].style.display = 'block' 
	else ulObjs[0].style.display = 'none' ;
}
