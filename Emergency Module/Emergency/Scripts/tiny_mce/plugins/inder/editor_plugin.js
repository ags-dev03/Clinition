(function () { tinymce.create('tinymce.plugins.inderPlugin', { init: function (ed, url) { ed.addCommand('mceExample', function () { ed.windowManager.open({ file: url + '/dummyForm.aspx', width: 320 + ed.getLang('example.delta_width', 0), height: 120 + ed.getLang('example.delta_height', 0), inline: 1 }, { plugin_url: url, some_custom_arg: 'custom arg' }) }); ed.addButton('inder', { title: 'Add Custom Fields', cmd: 'mceExample', image: url + '/img/example.png' }); ed.onNodeChange.add(function (ed, cm, n) { cm.setActive('inder', n.nodeName == 'IMG') }) }, createControl: function (n, cm) { switch (n) { case 'mylistbox': var mlb = cm.createListBox('mylistbox', { title: 'My list box', onselect: function (v) { tinyMCE.activeEditor.windowManager.alert('Value selected:' + v) } }); mlb.add('Some item 1', 'val1'); mlb.add('some item 2', 'val2'); mlb.add('some item 3', 'val3'); return mlb } return null }, getInfo: function () { return { longname: 'Khalsa plugin', author: 'Inderjeet Singh Khalsa', authorurl: 'http://khalsanews.org', infourl: 'www.isra-thepeople.com', version: "1.0"} } }); tinymce.PluginManager.add('inder', tinymce.plugins.inderPlugin) })();