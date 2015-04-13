Imports System.Net
Imports System.IO
Imports FlickrNet
Imports SimpleOAuth
Imports System.Xml
Imports System.Xml.XPath



Public Class Form1
    '' FLCIKR API KEY 4f60a04f101ef604ead9be84856d9519
    '' FLICKR API SECRET 4e3fc31dff403d28

   
    'Dim tokenStep1 As String = ""

    'Dim webRequestToken As WebRequest = WebRequest.Create(tokenStep1)

    'Dim tokenResponseStream As Stream = webRequestToken.GetResponse.GetResponseStream()

    'Dim httpDocument As HtmlDocument



    'Private Sub WriteNewDocument(httpDocument As HtmlDocument)
    '    If (WebBrowser1.Document IsNot Nothing) Then
    '        Dim doc As HtmlDocument = tokenResponseStream.Write.Document.OpenNew(True)
    '        doc.Write("<HTML><BODY>This is a new HTML document.</BODY></HTML>")
    '    End If
    'End Sub

    'Dim requestedTest As String = "https://api.flickr.com/services/rest/?method=flickr.photos.search&api_key=df52b9fa8e1632a530e801c286f20f7c&tags=St.+Louis%2C+MO&privacy_filter=1&safe_search=1&content_type=1&per_page=1&page=1&format=rest&auth_token=72157651508263228-ecffc79596f9a26f&api_sig=a8ace12d9ba340200b4567676e2664b7"

    'Dim flickrRequestPhoto As WebRequest = HttpWebRequest.Create(requestedTest)

    'Dim testResponseStream As Stream = flickrRequestPhoto.GetResponse.GetResponseStream()

    'Dim xmlReader As New XmlDocument
    '    xmlReader.Load(testResponseStream)





    Private Sub getFlickrInfoBtn_Click(sender As Object, e As EventArgs) Handles getFlickrInfoBtn.Click

        Try
            '' still need to rebuild using the flickr.photo.search method with custom search options similar to ones specified here. 
            '' this search request is for St. Louis, MO. this particular method does not need to be authorized. and most parameters are optional. 
            Dim requestedTest As String = "https://api.flickr.com/services/rest/?method=flickr.photos.search&api_key=df52b9fa8e1632a530e801c286f20f7c&tags=St.+Louis%2C+MO&privacy_filter=1&safe_search=1&content_type=1&per_page=1&page=1&format=rest&auth_token=72157651508263228-ecffc79596f9a26f&api_sig=a8ace12d9ba340200b4567676e2664b7"

            '' create the web request using the above url 
            Dim flickrRequestPhoto As WebRequest = WebRequest.Create(requestedTest)

            '' store the response stream in the created variable
            Dim testResponseStream As Stream = flickrRequestPhoto.GetResponse.GetResponseStream()

            '' create a xmldocument to format the response
            Dim xmlReader As New XmlDocument

            '' select the single xml-rpc response node
            Dim xmlRPCNode As XmlNode

            xmlReader.Load(testResponseStream)


            ' Save the document to a file. White space is 
            ' preserved (no white space). = true 
            xmlReader.PreserveWhitespace = False '' set to false to preserve the same format as formatted response 
            xmlReader.Save("data.xml") '' save the document in the debugger and use response to practice extracting info. 

            '' need to obtain xml-rpc siblings at the following values 
            ''secret path single node request = //photo/@secret this returns the location. Need to access inner text/xml by calling the innner text method 

            Dim url_builder_for_photo_secret_value_from_XML As XmlNode = xmlReader.SelectSingleNode("//photo/@secret")

            ''Still need to create 


            'server id' 
            Dim url_builder_for_photo_serverID_value_from_XML As XmlNode = xmlReader.SelectSingleNode("//photo/@server")

            'id'
            Dim url_builder_for_photo_ID_valueXML As XmlNode = xmlReader.SelectSingleNode("//photo/@id")

            ' Dim xmlRpcElemet As XmlNodeList = xmlReader.SelectNodes("//photos/@farm")
            '' use this to request farm value 
            Dim url_builder_for_farm_valueXML As XmlNode = xmlReader.SelectSingleNode("//photo/@farm")

            '' had alot of trouble trying to figure out how to set this up to read each element sibling. Had to use innerxml to extract sibling information on the specified path. 
            '' see example of return data at top of txtFile1

            MessageBox.Show("Here is Farm: " + url_builder_for_farm_valueXML.InnerXml + vbCrLf +
                            "Here is secret:" + url_builder_for_photo_secret_value_from_XML.InnerXml + vbCrLf _
                            + "Server ID: " + url_builder_for_photo_serverID_value_from_XML.InnerXml + vbCrLf +
                            "ID :" + url_builder_for_photo_ID_valueXML.InnerXml)

            '' confirmation that i obtained the desired values needed to request a photo. 

            '' now build a url request in this format. 

            ' https://farm{farm-id}.staticflickr.com/{server-id}/{id}_{secret}.jpg

            Dim url_PhotoRequest As String = String.Format("https://farm{0}.staticflickr.com/{1}/{2}_{3}.jpg", url_builder_for_farm_valueXML.InnerXml,
                                                           url_builder_for_photo_serverID_value_from_XML.InnerXml, url_builder_for_photo_ID_valueXML.InnerXml,
                                                           url_builder_for_photo_secret_value_from_XML.InnerXml)

            ''render this image to an image box to be displayed on a bing maps api. Or look into using Flickrs maps 

            '"Copy and past this link into a browser and see what photo is returned " + url_PhotoRequest)
            '' tried to display to messagebox , but could not click on its contents. 


            'error 404 page not found 
            '' need a try catch block when i reach this point. 
            ''todo get the rest of the elements from the initial response. 


            ''NOTE: each web request sent generates a different response of results. 


            'catch any exceptions and let me know what is going wrong. 
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
End Class
