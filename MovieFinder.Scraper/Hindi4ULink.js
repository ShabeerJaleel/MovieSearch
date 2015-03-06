var loadTime = (new Date()).getTime();
var key;
var prng;
var decryptedText;

/* jscrypt.js*/

function setKey() {

    var s = key;
    var hexDigits = "0123456789abcdefABCDEF";
    var hs = "", i, bogus = false;

    for (i = 0; i < s.length; i++) {
        var c = s.charAt(i);
        if (hexDigits.indexOf(c) >= 0) {
            hs += c;
        } else {
            bogus = true;
        }
    }
    if (bogus) {
        alert("Error: Improper character(s) in hexadecimal key.");
    }
    if (hs.length > (keySizeInBits / 4)) {
        //alert("Warning: hexadecimal key exceeds " +
        //  (keySizeInBits / 4) + " digit maximum; truncated.");
        document.addForm.key.value = hs = hs.slice(0, 64);
    } else {

        while (hs.length < (keySizeInBits / 4)) {
            hs += "0";
        }
    }
    key = hexToByteArray(hs);
    //	}
}

function Generate_key() {
    var i, j, k = "";

    var i, j, k = "";

    addEntropyTime();
    var seed = keyFromEntropy();

    var prng = new AESprng(seed);
    var hexDigits = "0123456789ABCDEF";

    for (i = 0; i < 64; i++) {
        k += hexDigits.charAt(prng.nextInt(15));
    }

    document.addForm.key.value = k;
    delete prng;
}

function Encrypt_text() {
    var v, i;

    if (document.addForm.key.value.length == 0) {
        // alert("Please specify a key with which to encrypt the message.");
        return;
    }
    if (document.getElementById("txtLink").value.length == 0) {
        //alert("No plain text to encrypt!  Please enter or paste plain text in the field above.");
        return;
    }
    document.getElementById("cipher").value = "";
    setKey();

    addEntropyTime();
    prng = new AESprng(keyFromEntropy());
    var plaintext = encode_utf8(document.getElementById("txtLink").value);

    //  Compute MD5 sum of message text and add to header

    md5_init();
    for (i = 0; i < plaintext.length; i++) {
        md5_update(plaintext.charCodeAt(i));
    }
    md5_finish();
    var header = "";
    for (i = 0; i < digestBits.length; i++) {
        header += String.fromCharCode(digestBits[i]);
    }


    i = plaintext.length;
    header += String.fromCharCode(i >>> 24);
    header += String.fromCharCode(i >>> 16);
    header += String.fromCharCode(i >>> 8);
    header += String.fromCharCode(i & 0xFF);

    var ct = rijndaelEncrypt(header + plaintext, key, "CBC");
    v = armour_hex(ct);
    document.getElementById("cipher").value = v;
    return v;
    delete prng;
}

function determineArmourType(s) {
    var kt, pcg, phex, pb64, pmin;

    pcg = s.indexOf(codegroupSentinel);
    phex = s.indexOf(hexSentinel);
    pb64 = s.indexOf(base64sent);
    if (pcg == -1) {
        pcg = s.length;
    }
    if (phex == -1) {
        phex = s.length;
    }
    if (pb64 == -1) {
        pb64 = s.length;
    }
    pmin = Math.min(pcg, Math.min(phex, pb64));
    if (pmin < s.length) {
        if (pmin == pcg) {
            kt = 0;
        } else if (pmin == phex) {
            kt = 1;
        } else {
            kt = 2;
        }
    } else {
        kt = 1;
    }
    return kt;
}

function Decrypt_text() {

    //document.getElementById("txtLink").value = "";
    setKey();
    var ct = new Array(), kt;
    kt = determineArmourType(cipher);
    if (kt == 0) {
        ct = disarm_codegroup(cipher);
    } else if (kt == 1) {
        ct = disarm_hex(cipher);
    } else if (kt == 2) {
        ct = disarm_base64(cipher);
    }

    var result = rijndaelDecrypt(ct, key, "CBC");

    var header = result.slice(0, 20);
    result = result.slice(20);

    var dl = (header[16] << 24) | (header[17] << 16) | (header[18] << 8) | header[19];
    if ((dl < 0) || (dl > result.length)) {
        //alert("Message (length " + result.length + ") truncated.  " +
        //	dl + " characters expected.");
        //	Try to sauve qui peut by setting length to entire message
        dl = result.length;
    }

    var i, plaintext = "";

    md5_init();
    for (i = 0; i < dl; i++) {
        plaintext += String.fromCharCode(result[i]);
        md5_update(result[i]);
    }
    md5_finish();

    for (i = 0; i < digestBits.length; i++) {
        if (digestBits[i] != header[i]) {
            //	alert("Message corrupted.  Checksum of decrypted message does not match.");
            break;
        }
    }

    //  That's it; plug plaintext into the result field

    //document.getElementById("txtLink").value = decode_utf8(plaintext);
    decryptedText = decode_utf8(plaintext);
}


/* utf-8.js*/


/*	Encoding and decoding of Unicode character strings as
UTF-8 byte streams.  */

//	UNICODE_TO_UTF8  --  Encode Unicode argument string as UTF-8 return value

function unicode_to_utf8(s) {
    var utf8 = "";

    for (var n = 0; n < s.length; n++) {
        var c = s.charCodeAt(n);

        if (c <= 0x7F) {
            //  0x00 - 0x7F:  Emit as single byte, unchanged
            utf8 += String.fromCharCode(c);
        } else if ((c >= 0x80) && (c <= 0x7FF)) {
            //  0x80 - 0x7FF:  Output as two byte code, 0xC0 in first byte
            //  	    	    	    	    	    0x80 in second byte
            utf8 += String.fromCharCode((c >> 6) | 0xC0);
            utf8 += String.fromCharCode((c & 0x3F) | 0x80);
        } else {
            // 0x800 - 0xFFFF:  Output as three bytes, 0xE0 in first byte
            //  	    	    	    	    	   0x80 in second byte
            //  	    	    	    	    	   0x80 in third byte
            utf8 += String.fromCharCode((c >> 12) | 0xE0);
            utf8 += String.fromCharCode(((c >> 6) & 0x3F) | 0x80);
            utf8 += String.fromCharCode((c & 0x3F) | 0x80);
        }
    }
    return utf8;
}

//	UTF8_TO_UNICODE  --  Decode UTF-8 argument into Unicode string return value

function utf8_to_unicode(utf8) {
    var s = "", i = 0, b1, b2, b2;

    while (i < utf8.length) {
        b1 = utf8.charCodeAt(i);
        if (b1 < 0x80) {	    // One byte code: 0x00 0x7F
            s += String.fromCharCode(b1);
            i++;
        } else if ((b1 >= 0xC0) && (b1 < 0xE0)) {	// Two byte code: 0x80 - 0x7FF
            b2 = utf8.charCodeAt(i + 1);
            s += String.fromCharCode(((b1 & 0x1F) << 6) | (b2 & 0x3F));
            i += 2;
        } else {	    	    // Three byte code: 0x800 - 0xFFFF
            b2 = utf8.charCodeAt(i + 1);
            b3 = utf8.charCodeAt(i + 2);
            s += String.fromCharCode(((b1 & 0xF) << 12) |
		    	    	    	 ((b2 & 0x3F) << 6) |
					 (b3 & 0x3F));
            i += 3;
        }
    }
    return s;
}

/*	ENCODE_UTF8  --  Encode string as UTF8 only if it contains
a character of 0x9D (Unicode OPERATING
SYSTEM COMMAND) or a character greater
than 0xFF.  This permits all strings
consisting exclusively of 8 bit
graphic characters to be encoded as
themselves.  We choose 0x9D as the sentinel
character as opposed to one of the more
logical PRIVATE USE characters because 0x9D
is not overloaded by the regrettable
"Windows-1252" character set.  Now such characters
don't belong in JavaScript strings, but you never
know what somebody is going to paste into a
text box, so this choice keeps Windows-encoded
strings from bloating to UTF-8 encoding.  */

function encode_utf8(s) {
    var i, necessary = false;

    for (i = 0; i < s.length; i++) {
        if ((s.charCodeAt(i) == 0x9D) ||
	    	(s.charCodeAt(i) > 0xFF)) {
            necessary = true;
            break;
        }
    }
    if (!necessary) {
        return s;
    }
    return String.fromCharCode(0x9D) + unicode_to_utf8(s);
}

/*  DECODE_UTF8  --  Decode a string encoded with encode_utf8
above.  If the string begins with the
sentinel character 0x9D (OPERATING
SYSTEM COMMAND), then we decode the
balance as a UTF-8 stream.  Otherwise,
the string is output unchanged, as
it's guaranteed to contain only 8 bit
characters excluding 0x9D.  */

function decode_utf8(s) {
    if ((s.length > 0) && (s.charCodeAt(0) == 0x9D)) {
        return utf8_to_unicode(s.substring(1));
    }
    return s;
}

/* md5.js*/

/*
*  md5.jvs 1.0b 27/06/96
*
* Javascript implementation of the RSA Data Security, Inc. MD5
* Message-Digest Algorithm.
*
* Copyright (c) 1996 Henri Torgemane. All Rights Reserved.
*
* Permission to use, copy, modify, and distribute this software
* and its documentation for any purposes and without
* fee is hereby granted provided that this copyright notice
* appears in all copies. 
*
* Of course, this soft is provided "as is" without express or implied
* warranty of any kind.

This version contains some trivial reformatting modifications
by John Walker.

*/

function array(n) {
    for (i = 0; i < n; i++) {
        this[i] = 0;
    }
    this.length = n;
}

/* Some basic logical functions had to be rewritten because of a bug in
* Javascript.. Just try to compute 0xffffffff >> 4 with it..
* Of course, these functions are slower than the original would be, but
* at least, they work!
*/

function integer(n) {
    return n % (0xffffffff + 1);
}

function shr(a, b) {
    a = integer(a);
    b = integer(b);
    if (a - 0x80000000 >= 0) {
        a = a % 0x80000000;
        a >>= b;
        a += 0x40000000 >> (b - 1);
    } else {
        a >>= b;
    }
    return a;
}

function shl1(a) {
    a = a % 0x80000000;
    if (a & 0x40000000 == 0x40000000) {
        a -= 0x40000000;
        a *= 2;
        a += 0x80000000;
    } else {
        a *= 2;
    }
    return a;
}

function shl(a, b) {
    a = integer(a);
    b = integer(b);
    for (var i = 0; i < b; i++) {
        a = shl1(a);
    }
    return a;
}

function and(a, b) {
    a = integer(a);
    b = integer(b);
    var t1 = a - 0x80000000;
    var t2 = b - 0x80000000;
    if (t1 >= 0) {
        if (t2 >= 0) {
            return ((t1 & t2) + 0x80000000);
        } else {
            return (t1 & b);
        }
    } else {
        if (t2 >= 0) {
            return (a & t2);
        } else {
            return (a & b);
        }
    }
}

function or(a, b) {
    a = integer(a);
    b = integer(b);
    var t1 = a - 0x80000000;
    var t2 = b - 0x80000000;
    if (t1 >= 0) {
        if (t2 >= 0) {
            return ((t1 | t2) + 0x80000000);
        } else {
            return ((t1 | b) + 0x80000000);
        }
    } else {
        if (t2 >= 0) {
            return ((a | t2) + 0x80000000);
        } else {
            return (a | b);
        }
    }
}

function xor(a, b) {
    a = integer(a);
    b = integer(b);
    var t1 = a - 0x80000000;
    var t2 = b - 0x80000000;
    if (t1 >= 0) {
        if (t2 >= 0) {
            return (t1 ^ t2);
        } else {
            return ((t1 ^ b) + 0x80000000);
        }
    } else {
        if (t2 >= 0) {
            return ((a ^ t2) + 0x80000000);
        } else {
            return (a ^ b);
        }
    }
}

function not(a) {
    a = integer(a);
    return 0xffffffff - a;
}

/* Here begin the real algorithm */

var state = new array(4);
var count = new array(2);
count[0] = 0;
count[1] = 0;
var buffer = new array(64);
var transformBuffer = new array(16);
var digestBits = new array(16);

var S11 = 7;
var S12 = 12;
var S13 = 17;
var S14 = 22;
var S21 = 5;
var S22 = 9;
var S23 = 14;
var S24 = 20;
var S31 = 4;
var S32 = 11;
var S33 = 16;
var S34 = 23;
var S41 = 6;
var S42 = 10;
var S43 = 15;
var S44 = 21;

function F(x, y, z) {
    return or(and(x, y), and(not(x), z));
}

function G(x, y, z) {
    return or(and(x, z), and(y, not(z)));
}

function H(x, y, z) {
    return xor(xor(x, y), z);
}

function I(x, y, z) {
    return xor(y, or(x, not(z)));
}

function rotateLeft(a, n) {
    return or(shl(a, n), (shr(a, (32 - n))));
}

function FF(a, b, c, d, x, s, ac) {
    a = a + F(b, c, d) + x + ac;
    a = rotateLeft(a, s);
    a = a + b;
    return a;
}

function GG(a, b, c, d, x, s, ac) {
    a = a + G(b, c, d) + x + ac;
    a = rotateLeft(a, s);
    a = a + b;
    return a;
}

function HH(a, b, c, d, x, s, ac) {
    a = a + H(b, c, d) + x + ac;
    a = rotateLeft(a, s);
    a = a + b;
    return a;
}

function II(a, b, c, d, x, s, ac) {
    a = a + I(b, c, d) + x + ac;
    a = rotateLeft(a, s);
    a = a + b;
    return a;
}

function transform(buf, offset) {
    var a = 0, b = 0, c = 0, d = 0;
    var x = transformBuffer;

    a = state[0];
    b = state[1];
    c = state[2];
    d = state[3];

    for (i = 0; i < 16; i++) {
        x[i] = and(buf[i * 4 + offset], 0xFF);
        for (j = 1; j < 4; j++) {
            x[i] += shl(and(buf[i * 4 + j + offset], 0xFF), j * 8);
        }
    }

    /* Round 1 */
    a = FF(a, b, c, d, x[0], S11, 0xd76aa478); /* 1 */
    d = FF(d, a, b, c, x[1], S12, 0xe8c7b756); /* 2 */
    c = FF(c, d, a, b, x[2], S13, 0x242070db); /* 3 */
    b = FF(b, c, d, a, x[3], S14, 0xc1bdceee); /* 4 */
    a = FF(a, b, c, d, x[4], S11, 0xf57c0faf); /* 5 */
    d = FF(d, a, b, c, x[5], S12, 0x4787c62a); /* 6 */
    c = FF(c, d, a, b, x[6], S13, 0xa8304613); /* 7 */
    b = FF(b, c, d, a, x[7], S14, 0xfd469501); /* 8 */
    a = FF(a, b, c, d, x[8], S11, 0x698098d8); /* 9 */
    d = FF(d, a, b, c, x[9], S12, 0x8b44f7af); /* 10 */
    c = FF(c, d, a, b, x[10], S13, 0xffff5bb1); /* 11 */
    b = FF(b, c, d, a, x[11], S14, 0x895cd7be); /* 12 */
    a = FF(a, b, c, d, x[12], S11, 0x6b901122); /* 13 */
    d = FF(d, a, b, c, x[13], S12, 0xfd987193); /* 14 */
    c = FF(c, d, a, b, x[14], S13, 0xa679438e); /* 15 */
    b = FF(b, c, d, a, x[15], S14, 0x49b40821); /* 16 */

    /* Round 2 */
    a = GG(a, b, c, d, x[1], S21, 0xf61e2562); /* 17 */
    d = GG(d, a, b, c, x[6], S22, 0xc040b340); /* 18 */
    c = GG(c, d, a, b, x[11], S23, 0x265e5a51); /* 19 */
    b = GG(b, c, d, a, x[0], S24, 0xe9b6c7aa); /* 20 */
    a = GG(a, b, c, d, x[5], S21, 0xd62f105d); /* 21 */
    d = GG(d, a, b, c, x[10], S22, 0x2441453); /* 22 */
    c = GG(c, d, a, b, x[15], S23, 0xd8a1e681); /* 23 */
    b = GG(b, c, d, a, x[4], S24, 0xe7d3fbc8); /* 24 */
    a = GG(a, b, c, d, x[9], S21, 0x21e1cde6); /* 25 */
    d = GG(d, a, b, c, x[14], S22, 0xc33707d6); /* 26 */
    c = GG(c, d, a, b, x[3], S23, 0xf4d50d87); /* 27 */
    b = GG(b, c, d, a, x[8], S24, 0x455a14ed); /* 28 */
    a = GG(a, b, c, d, x[13], S21, 0xa9e3e905); /* 29 */
    d = GG(d, a, b, c, x[2], S22, 0xfcefa3f8); /* 30 */
    c = GG(c, d, a, b, x[7], S23, 0x676f02d9); /* 31 */
    b = GG(b, c, d, a, x[12], S24, 0x8d2a4c8a); /* 32 */

    /* Round 3 */
    a = HH(a, b, c, d, x[5], S31, 0xfffa3942); /* 33 */
    d = HH(d, a, b, c, x[8], S32, 0x8771f681); /* 34 */
    c = HH(c, d, a, b, x[11], S33, 0x6d9d6122); /* 35 */
    b = HH(b, c, d, a, x[14], S34, 0xfde5380c); /* 36 */
    a = HH(a, b, c, d, x[1], S31, 0xa4beea44); /* 37 */
    d = HH(d, a, b, c, x[4], S32, 0x4bdecfa9); /* 38 */
    c = HH(c, d, a, b, x[7], S33, 0xf6bb4b60); /* 39 */
    b = HH(b, c, d, a, x[10], S34, 0xbebfbc70); /* 40 */
    a = HH(a, b, c, d, x[13], S31, 0x289b7ec6); /* 41 */
    d = HH(d, a, b, c, x[0], S32, 0xeaa127fa); /* 42 */
    c = HH(c, d, a, b, x[3], S33, 0xd4ef3085); /* 43 */
    b = HH(b, c, d, a, x[6], S34, 0x4881d05); /* 44 */
    a = HH(a, b, c, d, x[9], S31, 0xd9d4d039); /* 45 */
    d = HH(d, a, b, c, x[12], S32, 0xe6db99e5); /* 46 */
    c = HH(c, d, a, b, x[15], S33, 0x1fa27cf8); /* 47 */
    b = HH(b, c, d, a, x[2], S34, 0xc4ac5665); /* 48 */

    /* Round 4 */
    a = II(a, b, c, d, x[0], S41, 0xf4292244); /* 49 */
    d = II(d, a, b, c, x[7], S42, 0x432aff97); /* 50 */
    c = II(c, d, a, b, x[14], S43, 0xab9423a7); /* 51 */
    b = II(b, c, d, a, x[5], S44, 0xfc93a039); /* 52 */
    a = II(a, b, c, d, x[12], S41, 0x655b59c3); /* 53 */
    d = II(d, a, b, c, x[3], S42, 0x8f0ccc92); /* 54 */
    c = II(c, d, a, b, x[10], S43, 0xffeff47d); /* 55 */
    b = II(b, c, d, a, x[1], S44, 0x85845dd1); /* 56 */
    a = II(a, b, c, d, x[8], S41, 0x6fa87e4f); /* 57 */
    d = II(d, a, b, c, x[15], S42, 0xfe2ce6e0); /* 58 */
    c = II(c, d, a, b, x[6], S43, 0xa3014314); /* 59 */
    b = II(b, c, d, a, x[13], S44, 0x4e0811a1); /* 60 */
    a = II(a, b, c, d, x[4], S41, 0xf7537e82); /* 61 */
    d = II(d, a, b, c, x[11], S42, 0xbd3af235); /* 62 */
    c = II(c, d, a, b, x[2], S43, 0x2ad7d2bb); /* 63 */
    b = II(b, c, d, a, x[9], S44, 0xeb86d391); /* 64 */

    state[0] += a;
    state[1] += b;
    state[2] += c;
    state[3] += d;

}

function md5_init() {
    count[0] = count[1] = 0;
    state[0] = 0x67452301;
    state[1] = 0xefcdab89;
    state[2] = 0x98badcfe;
    state[3] = 0x10325476;
    for (i = 0; i < digestBits.length; i++) {
        digestBits[i] = 0;
    }
}

function md5_update(b) {
    var index, i;

    index = and(shr(count[0], 3), 0x3F);
    if (count[0] < 0xFFFFFFFF - 7) {
        count[0] += 8;
    } else {
        count[1]++;
        count[0] -= 0xFFFFFFFF + 1;
        count[0] += 8;
    }
    buffer[index] = and(b, 0xff);
    if (index >= 63) {
        transform(buffer, 0);
    }
}

function md5_finish() {
    var bits = new array(8);
    var padding;
    var i = 0, index = 0, padLen = 0;

    for (i = 0; i < 4; i++) {
        bits[i] = and(shr(count[0], (i * 8)), 0xFF);
    }
    for (i = 0; i < 4; i++) {
        bits[i + 4] = and(shr(count[1], (i * 8)), 0xFF);
    }
    index = and(shr(count[0], 3), 0x3F);
    padLen = (index < 56) ? (56 - index) : (120 - index);
    padding = new array(64);
    padding[0] = 0x80;
    for (i = 0; i < padLen; i++) {
        md5_update(padding[i]);
    }
    for (i = 0; i < 8; i++) {
        md5_update(bits[i]);
    }

    for (i = 0; i < 4; i++) {
        for (j = 0; j < 4; j++) {
            digestBits[i * 4 + j] = and(shr(state[i], (j * 8)), 0xFF);
        }
    }
}

/* End of the MD5 algorithm */


/* armour.js */

//  Varieties of ASCII armour for binary data

var maxLineLength = 64; 	    	// Maximum line length for armoured text

/*	    	    	    Hexadecimal Armour
    
A message is encoded in Hexadecimal armour by expressing its
bytes as a hexadecimal string which is prefixed by a sentinel
of "?HX?" and suffixed by "?H", then broken into lines no
longer than maxLineLength.  Armoured messages use lower case
letters for digits with decimal values of 0 through 15, but
either upper or lower case letters are accepted when decoding
a message.  The hexadecimal to byte array interconversion
routines in aes.js do most of the heavy lifting here.  */

var hexSentinel = "?HX?", hexEndSentinel = "?H";

//	Encode byte array in hexadecimal armour

function armour_hex(b) {
    var h = hexSentinel + byteArrayToHex(b) + hexEndSentinel;
    var t = "";
    while (h.length > maxLineLength) {
        //dump("h.length", h.length);
        t += h.substring(0, maxLineLength) + "\n";
        h = h.substring(maxLineLength, h.length);
    }
    //dump("h.final_length", h.length);
    t += h + "\n";
    return t;
}

/*	Decode string in hexadecimal armour to byte array.  If the
string supplied contains a start and/or end sentinel,
only characters within the sentinels will be decoded.
Non-hexadecimal digits are silently ignored, which
automatically handles line breaks.  We might want to
diagnose invalid characters as opposed to ignoring them.  */

function disarm_hex(s) {
    var hexDigits = "0123456789abcdefABCDEF";
    var hs = "", i;

    //  Extract hexadecimal data between sentinels, if present

    if ((i = s.indexOf(hexSentinel)) >= 0) {
        s = s.substring(i + hexSentinel.length, s.length);
    }
    if ((i = s.indexOf(hexEndSentinel)) >= 0) {
        s = s.substring(0, i);
    }

    //  Assemble string of valid hexadecimal digits

    for (i = 0; i < s.length; i++) {
        var c = s.charAt(i);
        if (hexDigits.indexOf(c) >= 0) {
            hs += c;
        }
    }
    //dump("hs", hs);
    return hexToByteArray(hs);
}

/*	    	    	    Codegroup Armour
    
Codegroup armour encodes a byte string into a sequence of five
letter code groups like spies used in the good old days.  The
first group of a message is always "ZZZZZ" and the last "YYYYY";
the decoding process ignores any text outside these start and
end sentinels.  Bytes are encoded as two letters in the range
"A" to "X", each encoding four bits of the byte.  Encoding uses
a pseudorandomly generated base letter and wraps around modulo
24 to spread encoded letters evenly through the alphabet.  (This
refinement is purely aesthetic; the base letter sequence is
identical for all messages and adds no security.  If the message
does not fill an even number of five letter groups, the last
group is padded to five letters with "Z" characters, which are
ignored when decoding.  */

var acgcl, acgt, acgg;

//	Output next codegroup, flushing current line if it's full

function armour_cg_outgroup() {
    if (acgcl.length > maxLineLength) {
        acgt += acgcl + "\n";
        acgcl = "";
    }
    if (acgcl.length > 0) {
        acgcl += " ";
    }
    acgcl += acgg;
    acgg = "";
}

/*	Add a letter to the current codegroup, emitting it when
it reaches five letters.  */

function armour_cg_outletter(l) {
    if (acgg.length >= 5) {
        armour_cg_outgroup();
    }
    acgg += l;
}

var codegroupSentinel = "ZZZZZ";

function armour_codegroup(b) {
    var charBase = ("A").charCodeAt(0);

    acgcl = codegroupSentinel;
    acgt = "";
    acgg = "";

    var cgrng = new LEcuyer(0xbadf00d);
    for (i = 0; i < b.length; i++) {
        var r = cgrng.nextInt(23);
        armour_cg_outletter(String.fromCharCode(charBase + ((((b[i] >> 4) & 0xF)) + r) % 24));
        r = cgrng.nextInt(23);
        armour_cg_outletter(String.fromCharCode(charBase + ((((b[i] & 0xF)) + r) % 24)));
    }
    delete cgrng;

    //  Generate nulls to fill final codegroup if required

    while (acgg.length < 5) {
        armour_cg_outletter("Z");
    }
    armour_cg_outgroup();

    //  Append terminator group

    acgg = "YYYYY";
    armour_cg_outgroup();

    //  Flush last line

    acgt += acgcl + "\n";

    return acgt;
}

var dcgs, dcgi;

/*	Obtain next "significant" character from message.  Characters
other than letters are silently ignored; both lower and upper
case letters are accepted.  */

function disarm_cg_insig() {
    while (dcgi < dcgs.length) {
        var c = dcgs.charAt(dcgi++).toUpperCase();
        if ((c >= "A") && (c <= "Z")) {
            //dump("c", c);
            return c;
        }
    }
    return "";
}

//	Decode a message in codegroup armour

function disarm_codegroup(s) {
    var b = new Array();
    var nz = 0, ba, bal = 0, c;

    dcgs = s;
    dcgi = 0;

    //  Search for initial group of "ZZZZZ"

    while (nz < 5) {
        c = disarm_cg_insig();

        if (c == "Z") {
            nz++;
        } else if (c == "") {
            nz = 0;
            break;
        } else {
            nz = 0;
        }
    }

    if (nz == 0) {
        alert("No codegroup starting symbol found in message.");
        return "";
    }

    /*  Decode letter pairs from successive groups
    and assemble into bytes.  */

    var charBase = ("A").charCodeAt(0);
    var cgrng = new LEcuyer(0xbadf00d);
    for (nz = 0; nz < 2; ) {
        c = disarm_cg_insig();
        //dump("c", c);

        if ((c == "Y") || (c == "")) {
            break;
        } else if (c != "Z") {
            var r = cgrng.nextInt(23);
            var n = c.charCodeAt(0) - charBase;
            n = (n + (24 - r)) % 24;
            //dump("n", n);
            if (nz == 0) {
                ba = (n << 4);
                nz++;
            } else {
                ba |= n;
                b[bal++] = ba;
                nz = 0;
            }
        }
    }
    delete cgrng;

    /*  Ponder how we escaped from the decoder loop and
    issue any requisite warnings.  */

    var kbo = "  Attempting decoding with data received.";
    if (nz != 0) {
        alert("Codegroup data truncated." + kbo);
    } else {
        if (c == "Y") {
            nz = 1;
            while (nz < 5) {
                c = disarm_cg_insig();
                if (c != "Y") {
                    break;
                }
                nz++;
            }
            if (nz != 5) {
                alert("Codegroup end group incomplete." + kbo);
            }
        } else {
            alert("Codegroup end group missing." + kbo);
        }
    }

    return b;
}

/*	    	    	    Base64 Armour
    
Base64 armour encodes a byte array as described in RFC 1341.  Sequences
of three bytes are encoded into groups of four characters from a set
of 64 consisting of the upper and lower case letters, decimal digits,
and the special characters "+" and "/".  If the input is not a multiple
of three characters, the end of the message is padded with one or two
"=" characters to indicate its actual length.  We prefix the armoured
message with "?b64" and append "?64b" to the end; if one or both
of these sentinels are present, text outside them is ignored.  You can
suppress the generation of sentinels in armour by setting base64addsent
false before calling armour_base64.  */


var base64code = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/",
    	base64sent = "?b64", base64esent = "?64b", base64addsent = true;

function armour_base64(b) {
    var b64t = "";
    var b64l = base64addsent ? base64sent : "";

    var i;
    for (i = 0; i <= b.length - 3; i += 3) {
        if ((b64l.length + 4) > maxLineLength) {
            b64t += b64l + "\n";
            b64l = "";
        }
        b64l += base64code.charAt(b[i] >> 2);
        b64l += base64code.charAt(((b[i] & 3) << 4) | (b[i + 1] >> 4));
        b64l += base64code.charAt(((b[i + 1] & 0xF) << 2) | (b[i + 2] >> 6));
        b64l += base64code.charAt(b[i + 2] & 0x3F);
    }

    //dump("b.length", b.length);  dump("i", i); dump("(b.length - i)", (b.length - i));
    if ((b.length - i) == 1) {
        b64l += base64code.charAt(b[i] >> 2);
        b64l += base64code.charAt(((b[i] & 3) << 4));
        b64l += "==";
    } else if ((b.length - i) == 2) {
        b64l += base64code.charAt(b[i] >> 2);
        b64l += base64code.charAt(((b[i] & 3) << 4) | (b[i + 1] >> 4));
        b64l += base64code.charAt(((b[i + 1] & 0xF) << 2));
        b64l += "=";
    }

    if ((b64l.length + 4) > maxLineLength) {
        b64t += b64l + "\n";
        b64l = "";
    }
    if (base64addsent) {
        b64l += base64esent;
    }
    b64t += b64l + "\n";
    return b64t;
}

function disarm_base64(s) {
    var b = new Array();
    var i = 0, j, c, shortgroup = 0, n = 0;
    var d = new Array();

    if ((j = s.indexOf(base64sent)) >= 0) {
        s = s.substring(j + base64sent.length, s.length);
    }
    if ((j = s.indexOf(base64esent)) >= 0) {
        s = s.substring(0, j);
    }

    /*  Ignore any non-base64 characters before the encoded
    data stream and skip the type sentinel if present.  */

    while (i < s.length) {
        if (base64code.indexOf(s.charAt(i)) != -1) {
            break;
        }
        i++;
    }

    /*  Decode the base64 data stream.  The decoder is
    terminated by the end of the input string or
    the occurrence of the explicit end sentinel.  */

    while (i < s.length) {
        for (j = 0; j < 4; ) {
            if (i >= s.length) {
                if (j > 0) {
                    alert("Base64 cipher text truncated.");
                    return b;
                }
                break;
            }
            c = base64code.indexOf(s.charAt(i));
            if (c >= 0) {
                d[j++] = c;
            } else if (s.charAt(i) == "=") {
                d[j++] = 0;
                shortgroup++;
            } else if (s.substring(i, i + base64esent.length) == base64esent) {
                //dump("s.substring(i, i + base64esent.length)", s.substring(i, i + base64esent.length));
                //dump("esent", i);
                i = s.length;
                continue;
            } else {
                //dump("s.substring(i, i + base64esent.length)", s.substring(i, i + base64esent.length));
                //dump("usent", i);
                // Might improve diagnosis of improper character in else clause here
            }
            i++;
        }
        //dump("d0", d[0]); dump("d1", d[1]); dump("d2", d[2]); dump("d3", d[3]); 
        //dump("shortgroup", shortgroup);
        //dump("n", n);
        if (j == 4) {
            b[n++] = ((d[0] << 2) | (d[1] >> 4)) & 0xFF;
            if (shortgroup < 2) {
                b[n++] = ((d[1] << 4) | (d[2] >> 2)) & 0xFF;
                //dump("(d[1] << 4) | (d[2] >> 2)", (d[1] << 4) | (d[2] >> 2));
                if (shortgroup < 1) {
                    b[n++] = ((d[2] << 6) | d[3]) & 0xFF;
                }
            }
        }
    }
    return b;
}


/*aes.js */

/* rijndael.js      Rijndael Reference Implementation

This is a modified version of the software described below,
produced in September 2003 by John Walker for use in the
JavsScrypt browser-based encryption package.  The principal
changes are replacing the original getRandomBytes function with
one which calls our pseudorandom generator (which must
be instantiated and seeded before the first call on getRandomBytes),
and changing keySizeInBits to 256.  Some code not required by the
JavsScrypt application has been commented out.  Please see
http://www.fourmilab.ch/javascrypt/ for further information on
JavaScrypt.
    
The following is the original copyright and application
information.

Copyright (c) 2001 Fritz Schneider
 
This software is provided as-is, without express or implied warranty.  
Permission to use, copy, modify, distribute or sell this software, with or
without fee, for any purpose and by any individual or organization, is hereby
granted, provided that the above copyright notice and this paragraph appear 
in all copies. Distribution as a part of an application or binary must
include the above copyright notice in the documentation and/or other materials
provided with the application or distribution.

As the above disclaimer notes, you are free to use this code however you
want. However, I would request that you send me an email 
(fritz /at/ cs /dot/ ucsd /dot/ edu) to say hi if you find this code useful
or instructional. Seeing that people are using the code acts as 
encouragement for me to continue development. If you *really* want to thank
me you can buy the book I wrote with Thomas Powell, _JavaScript:
_The_Complete_Reference_ :)

This code is an UNOPTIMIZED REFERENCE implementation of Rijndael. 
If there is sufficient interest I can write an optimized (word-based, 
table-driven) version, although you might want to consider using a 
compiled language if speed is critical to your application. As it stands,
one run of the monte carlo test (10,000 encryptions) can take up to 
several minutes, depending upon your processor. You shouldn't expect more
than a few kilobytes per second in throughput.

Also note that there is very little error checking in these functions. 
Doing proper error checking is always a good idea, but the ideal 
implementation (using the instanceof operator and exceptions) requires
IE5+/NS6+, and I've chosen to implement this code so that it is compatible
with IE4/NS4. 

And finally, because JavaScript doesn't have an explicit byte/char data 
type (although JavaScript 2.0 most likely will), when I refer to "byte" 
in this code I generally mean "32 bit integer with value in the interval 
[0,255]" which I treat as a byte.

See http://www-cse.ucsd.edu/~fritz/rijndael.html for more documentation
of the (very simple) API provided by this code.

Fritz Schneider
fritz at cs.ucsd.edu
 
*/


// Rijndael parameters --  Valid values are 128, 192, or 256

var keySizeInBits = 256;
var blockSizeInBits = 128;

//
// Note: in the following code the two dimensional arrays are indexed as
//       you would probably expect, as array[row][column]. The state arrays
//       are 2d arrays of the form state[4][Nb].


// The number of rounds for the cipher, indexed by [Nk][Nb]
var roundsArray = [, , , , [, , , , 10, , 12, , 14], ,
                        [, , , , 12, , 12, , 14], ,
                        [, , , , 14, , 14, , 14]];

// The number of bytes to shift by in shiftRow, indexed by [Nb][row]
var shiftOffsets = [, , , , [, 1, 2, 3], , [, 1, 2, 3], , [, 1, 3, 4]];

// The round constants used in subkey expansion
var Rcon = [
0x01, 0x02, 0x04, 0x08, 0x10, 0x20,
0x40, 0x80, 0x1b, 0x36, 0x6c, 0xd8,
0xab, 0x4d, 0x9a, 0x2f, 0x5e, 0xbc,
0x63, 0xc6, 0x97, 0x35, 0x6a, 0xd4,
0xb3, 0x7d, 0xfa, 0xef, 0xc5, 0x91];

// Precomputed lookup table for the SBox
var SBox = [
 99, 124, 119, 123, 242, 107, 111, 197, 48, 1, 103, 43, 254, 215, 171,
118, 202, 130, 201, 125, 250, 89, 71, 240, 173, 212, 162, 175, 156, 164,
114, 192, 183, 253, 147, 38, 54, 63, 247, 204, 52, 165, 229, 241, 113,
216, 49, 21, 4, 199, 35, 195, 24, 150, 5, 154, 7, 18, 128, 226,
235, 39, 178, 117, 9, 131, 44, 26, 27, 110, 90, 160, 82, 59, 214,
179, 41, 227, 47, 132, 83, 209, 0, 237, 32, 252, 177, 91, 106, 203,
190, 57, 74, 76, 88, 207, 208, 239, 170, 251, 67, 77, 51, 133, 69,
249, 2, 127, 80, 60, 159, 168, 81, 163, 64, 143, 146, 157, 56, 245,
188, 182, 218, 33, 16, 255, 243, 210, 205, 12, 19, 236, 95, 151, 68,
23, 196, 167, 126, 61, 100, 93, 25, 115, 96, 129, 79, 220, 34, 42,
144, 136, 70, 238, 184, 20, 222, 94, 11, 219, 224, 50, 58, 10, 73,
  6, 36, 92, 194, 211, 172, 98, 145, 149, 228, 121, 231, 200, 55, 109,
141, 213, 78, 169, 108, 86, 244, 234, 101, 122, 174, 8, 186, 120, 37,
 46, 28, 166, 180, 198, 232, 221, 116, 31, 75, 189, 139, 138, 112, 62,
181, 102, 72, 3, 246, 14, 97, 53, 87, 185, 134, 193, 29, 158, 225,
248, 152, 17, 105, 217, 142, 148, 155, 30, 135, 233, 206, 85, 40, 223,
140, 161, 137, 13, 191, 230, 66, 104, 65, 153, 45, 15, 176, 84, 187,
 22];

// Precomputed lookup table for the inverse SBox
var SBoxInverse = [
 82, 9, 106, 213, 48, 54, 165, 56, 191, 64, 163, 158, 129, 243, 215,
251, 124, 227, 57, 130, 155, 47, 255, 135, 52, 142, 67, 68, 196, 222,
233, 203, 84, 123, 148, 50, 166, 194, 35, 61, 238, 76, 149, 11, 66,
250, 195, 78, 8, 46, 161, 102, 40, 217, 36, 178, 118, 91, 162, 73,
109, 139, 209, 37, 114, 248, 246, 100, 134, 104, 152, 22, 212, 164, 92,
204, 93, 101, 182, 146, 108, 112, 72, 80, 253, 237, 185, 218, 94, 21,
 70, 87, 167, 141, 157, 132, 144, 216, 171, 0, 140, 188, 211, 10, 247,
228, 88, 5, 184, 179, 69, 6, 208, 44, 30, 143, 202, 63, 15, 2,
193, 175, 189, 3, 1, 19, 138, 107, 58, 145, 17, 65, 79, 103, 220,
234, 151, 242, 207, 206, 240, 180, 230, 115, 150, 172, 116, 34, 231, 173,
 53, 133, 226, 249, 55, 232, 28, 117, 223, 110, 71, 241, 26, 113, 29,
 41, 197, 137, 111, 183, 98, 14, 170, 24, 190, 27, 252, 86, 62, 75,
198, 210, 121, 32, 154, 219, 192, 254, 120, 205, 90, 244, 31, 221, 168,
 51, 136, 7, 199, 49, 177, 18, 16, 89, 39, 128, 236, 95, 96, 81,
127, 169, 25, 181, 74, 13, 45, 229, 122, 159, 147, 201, 156, 239, 160,
224, 59, 77, 174, 42, 245, 176, 200, 235, 187, 60, 131, 83, 153, 97,
 23, 43, 4, 126, 186, 119, 214, 38, 225, 105, 20, 99, 85, 33, 12,
125];

// This method circularly shifts the array left by the number of elements
// given in its parameter. It returns the resulting array and is used for 
// the ShiftRow step. Note that shift() and push() could be used for a more 
// elegant solution, but they require IE5.5+, so I chose to do it manually. 

function cyclicShiftLeft(theArray, positions) {
    var temp = theArray.slice(0, positions);
    theArray = theArray.slice(positions).concat(temp);
    return theArray;
}

// Cipher parameters ... do not change these
var Nk = keySizeInBits / 32;
var Nb = blockSizeInBits / 32;
var Nr = roundsArray[Nk][Nb];

// Multiplies the element "poly" of GF(2^8) by x. See the Rijndael spec.

function xtime(poly) {
    poly <<= 1;
    return ((poly & 0x100) ? (poly ^ 0x11B) : (poly));
}

// Multiplies the two elements of GF(2^8) together and returns the result.
// See the Rijndael spec, but should be straightforward: for each power of
// the indeterminant that has a 1 coefficient in x, add y times that power
// to the result. x and y should be bytes representing elements of GF(2^8)

function mult_GF256(x, y) {
    var bit, result = 0;

    for (bit = 1; bit < 256; bit *= 2, y = xtime(y)) {
        if (x & bit)
            result ^= y;
    }
    return result;
}

// Performs the substitution step of the cipher.  State is the 2d array of
// state information (see spec) and direction is string indicating whether
// we are performing the forward substitution ("encrypt") or inverse 
// substitution (anything else)

function byteSub(state, direction) {
    var S;
    if (direction == "encrypt")           // Point S to the SBox we're using
        S = SBox;
    else
        S = SBoxInverse;
    for (var i = 0; i < 4; i++)           // Substitute for every byte in state
        for (var j = 0; j < Nb; j++)
            state[i][j] = S[state[i][j]];
}

// Performs the row shifting step of the cipher.

function shiftRow(state, direction) {
    for (var i = 1; i < 4; i++)               // Row 0 never shifts
        if (direction == "encrypt")
            state[i] = cyclicShiftLeft(state[i], shiftOffsets[Nb][i]);
        else
            state[i] = cyclicShiftLeft(state[i], Nb - shiftOffsets[Nb][i]);

}

// Performs the column mixing step of the cipher. Most of these steps can
// be combined into table lookups on 32bit values (at least for encryption)
// to greatly increase the speed. 

function mixColumn(state, direction) {
    var b = [];                            // Result of matrix multiplications
    for (var j = 0; j < Nb; j++) {         // Go through each column...
        for (var i = 0; i < 4; i++) {        // and for each row in the column...
            if (direction == "encrypt")
                b[i] = mult_GF256(state[i][j], 2) ^          // perform mixing
               mult_GF256(state[(i + 1) % 4][j], 3) ^
               state[(i + 2) % 4][j] ^
               state[(i + 3) % 4][j];
            else
                b[i] = mult_GF256(state[i][j], 0xE) ^
               mult_GF256(state[(i + 1) % 4][j], 0xB) ^
               mult_GF256(state[(i + 2) % 4][j], 0xD) ^
               mult_GF256(state[(i + 3) % 4][j], 9);
        }
        for (var i = 0; i < 4; i++)          // Place result back into column
            state[i][j] = b[i];
    }
}

// Adds the current round key to the state information. Straightforward.

function addRoundKey(state, roundKey) {
    for (var j = 0; j < Nb; j++) {                 // Step through columns...
        state[0][j] ^= (roundKey[j] & 0xFF);         // and XOR
        state[1][j] ^= ((roundKey[j] >> 8) & 0xFF);
        state[2][j] ^= ((roundKey[j] >> 16) & 0xFF);
        state[3][j] ^= ((roundKey[j] >> 24) & 0xFF);
    }
}

// This function creates the expanded key from the input (128/192/256-bit)
// key. The parameter key is an array of bytes holding the value of the key.
// The returned value is an array whose elements are the 32-bit words that 
// make up the expanded key.

function keyExpansion(key) {
    var expandedKey = new Array();
    var temp;

    // in case the key size or parameters were changed...
    Nk = keySizeInBits / 32;
    Nb = blockSizeInBits / 32;
    Nr = roundsArray[Nk][Nb];

    for (var j = 0; j < Nk; j++)     // Fill in input key first
        expandedKey[j] =
      (key[4 * j]) | (key[4 * j + 1] << 8) | (key[4 * j + 2] << 16) | (key[4 * j + 3] << 24);

    // Now walk down the rest of the array filling in expanded key bytes as
    // per Rijndael's spec
    for (j = Nk; j < Nb * (Nr + 1); j++) {    // For each word of expanded key
        temp = expandedKey[j - 1];
        if (j % Nk == 0)
            temp = ((SBox[(temp >> 8) & 0xFF]) |
               (SBox[(temp >> 16) & 0xFF] << 8) |
               (SBox[(temp >> 24) & 0xFF] << 16) |
               (SBox[temp & 0xFF] << 24)) ^ Rcon[Math.floor(j / Nk) - 1];
        else if (Nk > 6 && j % Nk == 4)
            temp = (SBox[(temp >> 24) & 0xFF] << 24) |
             (SBox[(temp >> 16) & 0xFF] << 16) |
             (SBox[(temp >> 8) & 0xFF] << 8) |
             (SBox[temp & 0xFF]);
        expandedKey[j] = expandedKey[j - Nk] ^ temp;
    }
    return expandedKey;
}

// Rijndael's round functions... 

function Round(state, roundKey) {
    byteSub(state, "encrypt");
    shiftRow(state, "encrypt");
    mixColumn(state, "encrypt");
    addRoundKey(state, roundKey);
}

function InverseRound(state, roundKey) {
    addRoundKey(state, roundKey);
    mixColumn(state, "decrypt");
    shiftRow(state, "decrypt");
    byteSub(state, "decrypt");
}

function FinalRound(state, roundKey) {
    byteSub(state, "encrypt");
    shiftRow(state, "encrypt");
    addRoundKey(state, roundKey);
}

function InverseFinalRound(state, roundKey) {
    addRoundKey(state, roundKey);
    shiftRow(state, "decrypt");
    byteSub(state, "decrypt");
}

// encrypt is the basic encryption function. It takes parameters
// block, an array of bytes representing a plaintext block, and expandedKey,
// an array of words representing the expanded key previously returned by
// keyExpansion(). The ciphertext block is returned as an array of bytes.

function encrypt(block, expandedKey) {
    var i;
    if (!block || block.length * 8 != blockSizeInBits)
        return;
    if (!expandedKey)
        return;

    block = packBytes(block);
    addRoundKey(block, expandedKey);
    for (i = 1; i < Nr; i++)
        Round(block, expandedKey.slice(Nb * i, Nb * (i + 1)));
    FinalRound(block, expandedKey.slice(Nb * Nr));
    return unpackBytes(block);
}

// decrypt is the basic decryption function. It takes parameters
// block, an array of bytes representing a ciphertext block, and expandedKey,
// an array of words representing the expanded key previously returned by
// keyExpansion(). The decrypted block is returned as an array of bytes.

function decrypt(block, expandedKey) {
    var i;
    if (!block || block.length * 8 != blockSizeInBits)
        return;
    if (!expandedKey)
        return;

    block = packBytes(block);
    InverseFinalRound(block, expandedKey.slice(Nb * Nr));
    for (i = Nr - 1; i > 0; i--)
        InverseRound(block, expandedKey.slice(Nb * i, Nb * (i + 1)));
    addRoundKey(block, expandedKey);
    return unpackBytes(block);
}

/* !NEEDED
// This method takes a byte array (byteArray) and converts it to a string by
// applying String.fromCharCode() to each value and concatenating the result.
// The resulting string is returned. Note that this function SKIPS zero bytes
// under the assumption that they are padding added in formatPlaintext().
// Obviously, do not invoke this method on raw data that can contain zero
// bytes. It is really only appropriate for printable ASCII/Latin-1 
// values. Roll your own function for more robust functionality :)

function byteArrayToString(byteArray) {
var result = "";
for(var i=0; i<byteArray.length; i++)
if (byteArray[i] != 0) 
result += String.fromCharCode(byteArray[i]);
return result;
}
*/

// This function takes an array of bytes (byteArray) and converts them
// to a hexadecimal string. Array element 0 is found at the beginning of 
// the resulting string, high nibble first. Consecutive elements follow
// similarly, for example [16, 255] --> "10ff". The function returns a 
// string.

function byteArrayToHex(byteArray) {
    var result = "";
    if (!byteArray)
        return;
    for (var i = 0; i < byteArray.length; i++)
        result += ((byteArray[i] < 16) ? "0" : "") + byteArray[i].toString(16);

    return result;
}

// This function converts a string containing hexadecimal digits to an 
// array of bytes. The resulting byte array is filled in the order the
// values occur in the string, for example "10FF" --> [16, 255]. This
// function returns an array. 

function hexToByteArray(hexString) {
    var byteArray = [];
    if (hexString.length % 2)             // must have even length
        return;
    if (hexString.indexOf("0x") == 0 || hexString.indexOf("0X") == 0)
        hexString = hexString.substring(2);
    for (var i = 0; i < hexString.length; i += 2)
        byteArray[Math.floor(i / 2)] = parseInt(hexString.slice(i, i + 2), 16);
    return byteArray;
}

// This function packs an array of bytes into the four row form defined by
// Rijndael. It assumes the length of the array of bytes is divisible by
// four. Bytes are filled in according to the Rijndael spec (starting with
// column 0, row 0 to 3). This function returns a 2d array.

function packBytes(octets) {
    var state = new Array();
    if (!octets || octets.length % 4)
        return;

    state[0] = new Array(); state[1] = new Array();
    state[2] = new Array(); state[3] = new Array();
    for (var j = 0; j < octets.length; j += 4) {
        state[0][j / 4] = octets[j];
        state[1][j / 4] = octets[j + 1];
        state[2][j / 4] = octets[j + 2];
        state[3][j / 4] = octets[j + 3];
    }
    return state;
}

// This function unpacks an array of bytes from the four row format preferred
// by Rijndael into a single 1d array of bytes. It assumes the input "packed"
// is a packed array. Bytes are filled in according to the Rijndael spec. 
// This function returns a 1d array of bytes.

function unpackBytes(packed) {
    var result = new Array();
    for (var j = 0; j < packed[0].length; j++) {
        result[result.length] = packed[0][j];
        result[result.length] = packed[1][j];
        result[result.length] = packed[2][j];
        result[result.length] = packed[3][j];
    }
    return result;
}

// This function takes a prospective plaintext (string or array of bytes)
// and pads it with pseudorandom bytes if its length is not a multiple of the block 
// size. If plaintext is a string, it is converted to an array of bytes
// in the process. The type checking can be made much nicer using the 
// instanceof operator, but this operator is not available until IE5.0 so I 
// chose to use the heuristic below. 

function formatPlaintext(plaintext) {
    var bpb = blockSizeInBits / 8;               // bytes per block
    var i;

    // if primitive string or String instance
    if ((!((typeof plaintext == "object") &&
        ((typeof (plaintext[0])) == "number"))) &&
      ((typeof plaintext == "string") || plaintext.indexOf)) {
        plaintext = plaintext.split("");
        // Unicode issues here (ignoring high byte)
        for (i = 0; i < plaintext.length; i++)
            plaintext[i] = plaintext[i].charCodeAt(0) & 0xFF;
    }

    i = plaintext.length % bpb;
    if (i > 0) {
        plaintext = plaintext.concat(getRandomBytes(bpb - i));
    }

    return plaintext;
}

// Returns an array containing "howMany" random bytes.

function getRandomBytes(howMany) {
    var i, bytes = new Array();

    for (i = 0; i < howMany; i++) {
        bytes[i] = prng.nextInt(255);
    }
    return bytes;
}

// rijndaelEncrypt(plaintext, key, mode)
// Encrypts the plaintext using the given key and in the given mode. 
// The parameter "plaintext" can either be a string or an array of bytes. 
// The parameter "key" must be an array of key bytes. If you have a hex 
// string representing the key, invoke hexToByteArray() on it to convert it 
// to an array of bytes. The third parameter "mode" is a string indicating
// the encryption mode to use, either "ECB" or "CBC". If the parameter is
// omitted, ECB is assumed.
// 
// An array of bytes representing the cihpertext is returned. To convert 
// this array to hex, invoke byteArrayToHex() on it.

function rijndaelEncrypt(plaintext, key, mode) {
    var expandedKey, i, aBlock;
    var bpb = blockSizeInBits / 8;          // bytes per block
    var ct;                                 // ciphertext

    if (!plaintext || !key)
        return;
    if (key.length * 8 != keySizeInBits)
        return;
    if (mode == "CBC") {
        ct = getRandomBytes(bpb);             // get IV
        //dump("IV", byteArrayToHex(ct));
    } else {
        mode = "ECB";
        ct = new Array();
    }

    // convert plaintext to byte array and pad with zeros if necessary. 
    plaintext = formatPlaintext(plaintext);

    expandedKey = keyExpansion(key);

    for (var block = 0; block < plaintext.length / bpb; block++) {
        aBlock = plaintext.slice(block * bpb, (block + 1) * bpb);
        if (mode == "CBC") {
            for (var i = 0; i < bpb; i++) {
                aBlock[i] ^= ct[(block * bpb) + i];
            }
        }
        ct = ct.concat(encrypt(aBlock, expandedKey));
    }

    return ct;
}

// rijndaelDecrypt(ciphertext, key, mode)
// Decrypts the using the given key and mode. The parameter "ciphertext" 
// must be an array of bytes. The parameter "key" must be an array of key 
// bytes. If you have a hex string representing the ciphertext or key, 
// invoke hexToByteArray() on it to convert it to an array of bytes. The
// parameter "mode" is a string, either "CBC" or "ECB".
// 
// An array of bytes representing the plaintext is returned. To convert 
// this array to a hex string, invoke byteArrayToHex() on it. To convert it 
// to a string of characters, you can use byteArrayToString().

function rijndaelDecrypt(ciphertext, key, mode) {
    var expandedKey;
    var bpb = blockSizeInBits / 8;          // bytes per block
    var pt = new Array();                   // plaintext array
    var aBlock;                             // a decrypted block
    var block;                              // current block number

    if (!ciphertext || !key || typeof ciphertext == "string")
        return;
    if (key.length * 8 != keySizeInBits)
        return;
    if (!mode) {
        mode = "ECB";                         // assume ECB if mode omitted
    }

    expandedKey = keyExpansion(key);

    // work backwards to accomodate CBC mode 
    for (block = (ciphertext.length / bpb) - 1; block > 0; block--) {
        aBlock =
     decrypt(ciphertext.slice(block * bpb, (block + 1) * bpb), expandedKey);
        if (mode == "CBC")
            for (var i = 0; i < bpb; i++)
                pt[(block - 1) * bpb + i] = aBlock[i] ^ ciphertext[(block - 1) * bpb + i];
        else
            pt = aBlock.concat(pt);
    }

    // do last block if ECB (skips the IV in CBC)
    if (mode == "ECB")
        pt = decrypt(ciphertext.slice(0, bpb), expandedKey).concat(pt);

    return pt;
}



Decrypt_text();