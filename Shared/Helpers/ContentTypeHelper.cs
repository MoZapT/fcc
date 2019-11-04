using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Helpers
{
    public static class ContentTypeHelper
    {
        public static bool IsSupported(string contentType)
        {
            bool isSupported = false;

            if (IsImage(contentType)    ||
                IsSound(contentType)    ||
                IsDocument(contentType) ||
                IsVideo(contentType))
            {
                isSupported = true;
            }

            return isSupported;
        }

        public static bool IsImage(string contentType)
        {
            var isValid = false;

            switch (contentType)
            {
                case "image/gif":
                case "image/bmp":
                case "image/tiff":
                case "image/png":
                case "image/jpeg":
                case "application/pdf":
                    isValid = true;
                    break;
                default:
                    break;
            }

            return isValid;

            //compiled source code    image / cis - cod   cod
            //      image file image/ ief   ief
            //      JPEG image image/ jpeg  jpe
            //      JPEG file interchange format image/ pipeg     jfif
            //    scalable vector graphic     image / svg + xml   svg
            //Sun raster graphic  image / x - cmu - raster  ras
            //Corel metafile exchange image file  image / x - cmx     cmx
            //    icon    image / x - icon    ico
            //        portable any map image image/ x - portable - anymap     pnm
            //            portable bitmap image   image / x - portable - bitmap     pbm
            //                  portable graymap image  image / x - portable - graymap    pgm
            //                        portable pixmap image   image / x - portable - pixmap     ppm
            //                              RGB bitmap image/ x - rgb     rgb
            //                                X11 bitmap image/ x - xbitmap     xbm
            //                                  X11 pixmap image/ x - xpixmap     xpm
            //X - Windows dump image    image / x - xwindowdump     xwd
        }

        public static bool IsVideo(string contentType)
        {
            var isValid = false;

            switch (contentType)
            {
                case "video/mpeg":
                case "video/mp4":
                case "video/quicktime":
                case "video/x-msvideo":
                case "video/x-sgi-movie":
                    isValid = true;
                    break;
                default:
                    break;
            }

            return isValid;

            //        Logos library system file video/ x - la - asf  lsf
            //           streaming media shortcut    video / x - la - asf  lsx
            //                 advanced systems format file video/ x - ms - asf  asf
            //                   ActionScript remote document    video / x - ms - asf  asr
            //                         Microsoft ASF redirector file video/ x - ms - asf  asx
        }

        public static bool IsSound(string contentType)
        {
            var isValid = false;

            switch (contentType)
            {
                case "audio/mpeg":
                case "audio/x-wav":
                case "audio/mpeg3":
                case "audio/x-mpeg3":
                    isValid = true;
                    break;
                default:
                    break;
            }

            return isValid;

            //            audio file  audio / basic     au
            //  sound file audio/ basic     snd
            //  midi file audio/ mid   mid
            // media processing server studio audio/ mid   rmi
            //audio interchange file format audio/ x - aiff    aif
            //compressed audio interchange file audio/ x - aiff    aifc
            // audio interchange file format audio/ x - aiff    aiff
            //  media playlist file     audio / x - mpegurl     m3u
            //      Real Audio file     audio / x - pn - realaudio    ra
            //            Real Audio metadata file audio/ x - pn - realaudio    ram
        }

        public static bool IsDocument(string contentType)
        {
            var isValid = false;

            switch (contentType)
            {
                case "application/msword":
                case "application/pdf":
                case "application/csv":
                case "application/rtf":
                case "application/vnd.ms-excel":
                case "application/vnd.ms-powerpoint":
                case "application/zip":
                case "text/plain":
                case "text/html":
                case "text/richtext":
                    isValid = true;
                    break;
                default:
                    break;
            }

            return isValid;

            //            Corel Envoy     application / envoy   evy
            //  fractal image file  application / fractals    fif
            //    Windows print spool file application/ futuresplash    spl
            //  HTML application application/ hta     hta
            //Atari ST Program    application / internet - property - stream    acx
            //      BinHex encoded file     application / mac - binhex40    hqx
            //           Binary file application/ octet - stream *
            //       binary disk image   application / octet - stream    bin
            //           Java class file     application/octet-stream class
            //        Disk Masher image   application/octet-stream dms
            //executable file     application/octet-stream exe
            //LHARC compressed archive application/octet-stream lha
            //LZH compressed file application/octet-stream lzh
            //CALS raster image application/oda oda
            //ActiveX script  application/olescript axs
            //Outlook profile file application/pics-rules prf
            //certificate request file application/pkcs10 p10
            //certificate revocation list file    application/pkix-crl crl
            //Adobe Illustrator file application/postscript ai
            //postscript file     application/postscript eps
            //postscript file     application/postscript ps
            //set payment initiation application/set-payment-initiation setpay
            //set registration initiation application/set-registration-initiation setreg
            //Outlook mail message application/vnd.ms-outlook msg
            //serialized certificate store file   application/vnd.ms-pkicertstore sst
            //Windows catalog file application/vnd.ms-pkiseccat cat
            //stereolithography file  application/vnd.ms-pkistl stl
            //Microsoft Project file application/vnd.ms-project mpp
            //WordPerfect macro   application/vnd.ms-works wcm
            //Microsoft Works database application/vnd.ms-works wdb
            //Microsoft Works spreadsheet application/vnd.ms-works wks
            //Microsoft Works word processsor document application/vnd.ms-works wps
            //Windows help file application/winhlp hlp
            //binary CPIO archive application/x-bcpio bcpio
            //computable document format file     application/x-cdf cdf
            //Unix compressed file application/x-compress z
            //gzipped tar file application/x-compressed tgz
            //Unix CPIO archive application/x-cpio cpio
            //Photoshop custom shapes file    application/x-csh csh
            //Kodak RAW image file    application/x-director dcr
            //Adobe Director movie application/x-director dir
            //Macromedia Director movie application/x-director dxr
            //device independent format file  application/x-dvi dvi
            //Gnu tar archive application/x-gtar gtar
            //Gnu zipped archive application/x-gzip gz
            //hierarchical data format file   application/x-hdf hdf
            //internet settings file application/x-internet-signup ins
            //IIS internet service provider settings application/x-internet-signup isp
            //ARC+ architectural file     application/x-iphone iii
            //JavaScript file     application/x-javascript js
            //LaTex document  application/x-latex latex
            //Microsoft Access database application/x-msaccess mdb
            //Windows CardSpace file application/x-mscardfile crd
            //CrazyTalk clip file application/x-msclip clp
            //dynamic link library application/x-msdownload dll
            //Microsoft media viewer file     application/x-msmediaview m13
            //Steuer2001 file     application/x-msmediaview m14
            //multimedia viewer book source file application/x-msmediaview mvb
            //Windows meta file application/x-msmetafile wmf
            //Microsoft Money file application/x-msmoney mny
            //Microsoft Publisher file application/x-mspublisher pub
            //Turbo Tax tax schedule list application/x-msschedule scd
            //FTR media file application/x-msterminal trm
            //Microsoft Write file application/x-mswrite wri
            //computable document format file     application/x-netcdf cdf
            //Mastercam numerical control file    application/x-netcdf nc
            //MSX computers archive format    application/x-perfmon pma
            //performance monitor counter file    application/x-perfmon pmc
            //process monitor log file    application/x-perfmon pml
            //Avid persistant media record file application/x-perfmon pmr
            //Pegasus Mail draft stored message application/x-perfmon pmw
            //personal information exchange file  application/x-pkcs12 p12
            //PKCS #12 certificate file 	application/x-pkcs12 	pfx
            //PKCS #7 certificate file 	application/x-pkcs7-certificates 	p7b
            //software publisher certificate file     application/x-pkcs7-certificates spc
            //certificate request response file   application/x-pkcs7-certreqresp p7r
            //PKCS #7 certificate file 	application/x-pkcs7-mime 	p7c
            //digitally encrypted message     application/x-pkcs7-mime p7m
            //digitally signed email message  application/x-pkcs7-signature p7s
            //Bash shell script application/x-sh sh
            //Unix shar archive application/x-shar shar
            //Flash file  application/x-shockwave-flash swf
            //Stuffit archive file application/x-stuffit sit
            //system 5 release 4 CPIO file    application/x-sv4cpio sv4cpio
            //system 5 release 4 CPIO checksum data application/x-sv4crc sv4crc
            //consolidated Unix file archive  application/x-tar tar
            //Tcl script  application/x-tcl tcl
            //LaTeX source document application/x-tex tex
            //LaTeX info document application/x-texinfo texi
            //LaTeX info document application/x-texinfo texinfo
            //unformatted manual page application/x-troff roff
            //Turing source code file     application/x-troff t
            //TomeRaider 2 ebook file     application/x-troff tr
            //Unix manual     application/x-troff-man man
            //readme text file application/x-troff-me me
            //3ds Max script file     application/x-troff-ms ms
            //uniform standard tape archive format file   application/x-ustar ustar
            //source code     application/x-wais-source src
            //internet security certificate application/x-x509-ca-cert cer
            //security certificate    application/x-x509-ca-cert crt
            //DER certificate file application/x-x509-ca-cert der
            //public key security object application/ynd.ms-pkipko pko
            //            Cascading Style Sheet text/css css
            //H.323 internet telephony file text/h323 	323
            //Exchange streaming media file   text/html stm
            //NetMeeting user location service file text/iuls uls
            //BASIC source code file  text/plain bas
            //C/C++ source code file text/plain c
            //C/C++/Objective C header file   text/plain h
            //Scitext continuous tone file    text/scriptlet sct
            //tab separated values file   text/tab-separated-values tsv
            //hypertext template file text/webviewhtml htt
            //HTML component file text/x-component htc
            //TeX font encoding file  text/x-setext etx
            //vCard file  text/x-vcard vcf
        }
    }
}
