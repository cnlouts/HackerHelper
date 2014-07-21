#-*- encoding:utf-8 -*-
import hashlib
import base64
import urllib
from lib.utils.log import Logger

class encryptHelper(object):
    """description of class"""

    def md5encode(strCode):
        result = ''
        try:
            result = hashlib.md5(strCode).hexdigest()
        except Exception as e:
            Logger.Error(e)
        return result

    def sha1encode(self,strCode):
        result = ''
        try:
            result = hashlib.sha1(strCode).hexdigest()
        except Exception as e:
            Logger.Error(e)
        return result

    def sha224encode(self,strCode):
        result = ''
        try:
            result = hashlib.sha224(strCode).hexdigest()
        except Exception as e:
            Logger.Error(e)
        return result

    def sha256encode(self,strCode):
        result = ''
        try:
            result = hashlib.sha256(strCode).hexdigest()
        except Exception as e:
            Logger.Error(e)
        return result

    def sha384encode(self,strCode):
        result = ''
        try:
            result = hashlib.sha384(strCode).hexdigest()
        except Exception as e:
            Logger.Error(e)
        return result

    def sha512encode(self,strCode):
        result = ''
        try:
            result = hashlib.sha512(strCode).hexdigest()
        except Exception as e:
            Logger.Error(e)
        return result

    def base64encode(self,strCode):
        result = ''
        try:
            result = base64.b64encode(strCode)
        except Exception as e:
            Logger.Error(e)
        return result

    def base64decode(self,strCode):
        result = ''
        try:
            result = base64.b64decode(strCode)
        except Exception as e:
            Logger.Error(e)
        return result

    def urlencode(self,strCode):
        result = ''
        try:
            result = urllib.quote(strCode)
        except Exception as e:
            Logger.Error(e)
        return result

    def urldecode(self,strCode):
        result = ''
        try:
            result = urllib.unquote(strCode)
        except Exception as e:
            Logger.Error(e)
        return result

    def asciiC2I(self,strCode):
        '''
        :retuan a char
        '''
        result = ''
        if len(strCode) == 1:
            try:
                result = ord(strCode)
            except Exception as e:
                Logger.Error(e)
        return result

encryptHelp = encryptHelper()