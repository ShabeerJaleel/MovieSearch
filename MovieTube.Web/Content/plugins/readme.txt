setup :
- update new proxy.swf to enable license (http://gkplugins.com/download/PROXY.zip)
- put .htaccess, license.php, license.swf at same folder with pluginslist.xml
- open pluginslist.xml
- insert <plugins url="gkplugins_license.swf"/>  at first plugin

Limit embed code
- open pluginslist.xml
- insert <plugins url="gkplugins_license.swf?embeddenied=site1.com"/> (allow all site but site1.com can not use embed code from your site)
- insert <plugins url="gkplugins_license.swf?embedallow=site1.com"/> (denied all site use embed code from your site but site1.com is allow)
- insert <plugins url="gkplugins_license.swf?embedallow=*"/> (allow all site)
- if you don't insert embeddenied or embedallow, default is denied all site
- insert multi site with character (,)  Example : <plugins url="gkplugins_license.swf?embedallow=site1.com,site2.com"/>

note :
- use htaccess to license don't save at cache browser, so it help to user update new license when license change
- if htaccess not work, delete file .htaccess, license.php and rename license.swf to gkplugins_license.swf

Video Tutorial : http://www.youtube.com/watch?v=Lau-PGLmXBA