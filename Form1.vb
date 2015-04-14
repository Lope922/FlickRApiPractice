Imports System.Net
Imports System.IO
Imports FlickrNet
Imports SimpleOAuth
Imports System.Xml
Imports System.Xml.XPath

'' still need to add a form1 event loader that handles authentication of temp APIKey. 
'' also need to setup background workers and handle errors


Public Class Form1
    '' FLCIKR API KEY 4f60a04f101ef604ead9be84856d9519
    '' FLICKR API SECRET 4e3fc31dff403d28

    Public Function city() As String
        Dim cityFromTextBox As String
        cityFromTextBox = txtBoxCityName.Text

        If cityFromTextBox.Contains(" ") Then
            cityFromTextBox.Replace(" ", "+")
            Return cityFromTextBox
        End If
    End Function

    Public Function state() As String
        Dim stateFromComboBox As String
        stateFromComboBox = CStr(stateComboBox.SelectedItem())
        Return stateFromComboBox
    End Function

    Public Function callCityAndState() As String
        'city()
        'state()

        MessageBox.Show("city and state " + city() + state())
    End Function


    Private Sub getFlickrInfoBtn_Click(sender As Object, e As EventArgs) Handles getFlickrInfoBtn.Click
        '' call city and state fuctions 
        ' callCityAndState()

        '' base api method use 
        '        https://api.flickr.com/services/rest/?method=flickr.photos.search
        '' api key required to make request. This is a temp key expires every 24 hours
        '&api_key=348389c8e6dfd154eeccac983fb067b3

        '&tags=St.+Paul%2C+MN&safe_search=1

        '&content_type=1

        '&lat=44.97010040
        '&lon=-93.08209991

        '&radius=10
        '&radius_units=mi

        'only asking for 3 requests per page 
        '&per_page=3
        '&page=1

        '' returned in a rest -xml format
        '&format=rest"



        Try

            '' still need to rebuild using the flickr.photo.search method with custom search options similar to ones specified here. 
            '' this search request is for St. Louis, MO. this particular method does not need to be authorized. and most parameters are optional. 
            Dim requestedTest As String = "https://api.flickr.com/services/rest/?method=flickr.photos.search&api_key=c4471b9774deb717aa6c61f6b88e259a&tags=St.Paul&per_page=3&page=1&format=rest"

            '' create the web request using the above url 
            Dim flickrRequestPhoto As WebRequest = WebRequest.Create(requestedTest)

            '' store the response stream in the created variable
            Dim testResponseStream As Stream = flickrRequestPhoto.GetResponse.GetResponseStream()

            '' create a xmldocument to format the response
            Dim xmlReader As New XmlDocument

            '' select the single xml-rpc response node
            ' Dim xmlRPCNode As XmlNode

            xmlReader.Load(testResponseStream)


            ' Save the document to a file. White space is 
            ' preserved (no white space). = true 
            xmlReader.PreserveWhitespace = False '' set to false to preserve the same format as formatted response 
            xmlReader.Save("data.xml") '' save the document in the debugger and use response to practice extracting info. 

            '' need to obtain xml-rpc siblings at the following values 
            ''secret path single node request = //photo/@secret this returns the location. Need to access inner text/xml by calling the innner text method 

            Dim url_builder_for_photo_secret1_value_from_XML As XmlNode = xmlReader.SelectSingleNode("//photo/@secret")
            Dim url_builder_for_photo_secret2_value_from_XML As XmlNode = xmlReader.SelectSingleNode("//photo[2]/@secret")
            Dim url_builder_for_photo_secret3_value_from_XML As XmlNode = xmlReader.SelectSingleNode("//photo[3]/@secret")

            'server id' 
            Dim url_builder_for_photo_serverID1_value_from_XML As XmlNode = xmlReader.SelectSingleNode("//photo/@server")
            Dim url_builder_for_photo_serverID2_value_from_XML As XmlNode = xmlReader.SelectSingleNode("//photo[2]/@server")
            Dim url_builder_for_photo_serverID3_value_from_XML As XmlNode = xmlReader.SelectSingleNode("//photo[3]/@server")
            'id'
            Dim url_builder_for_photo_ID1_valueXML As XmlNode = xmlReader.SelectSingleNode("//photo/@id")
            Dim url_builder_for_photo_ID2_valueXML As XmlNode = xmlReader.SelectSingleNode("//photo[2]/@id")
            Dim url_builder_for_photo_ID3_valueXML As XmlNode = xmlReader.SelectSingleNode("//photo[3]/@id")

            ' Dim xmlRpcElemet As XmlNodeList = xmlReader.SelectNodes("//photos/@farm")

            '' use this to request farm value 
            Dim url_builder_for_farm1_valueXML As XmlNode = xmlReader.SelectSingleNode("//photo/@farm")
            Dim url_builder_for_farm2_valueXML As XmlNode = xmlReader.SelectSingleNode("//photo[2]/@farm")
            Dim url_builder_for_farm3_valueXML As XmlNode = xmlReader.SelectSingleNode("//photo[3]/@farm")


            '' had alot of trouble trying to figure out how to set this up to read each element sibling. Had to use innerxml to extract sibling information on the specified path. 
            '' see example of return data at top of txtFile1

            'MessageBox.Show("Here is Farm: " + url_builder_for_farm1_valueXML.InnerXml + vbCrLf +
            '                "Here is secret:" + url_builder_for_photo_secret1_value_from_XML.InnerXml + vbCrLf _
            '                + "Server ID: " + url_builder_for_photo_serverID1_value_from_XML.InnerXml + vbCrLf +
            '                "ID :" + url_builder_for_photo_ID1_valueXML.InnerXml)

            '' confirmation that i obtained the desired values needed to request a photo. 

            '' now build a url request in this format. 

            ' https://farm{farm-id}.staticflickr.com/{server-id}/{id}_{secret}.jpg

            'For Each number As Integer In 1 to 3

            Dim url_PhotoRequest1 As String = String.Format("https://farm{0}.staticflickr.com/{1}/{2}_{3}.jpg", url_builder_for_farm1_valueXML.InnerXml,
                                                               url_builder_for_photo_serverID1_value_from_XML.InnerXml, url_builder_for_photo_ID1_valueXML.InnerXml,
                                                               url_builder_for_photo_secret1_value_from_XML.InnerXml)

            Dim url_PhotoRequest2 As String = String.Format("https://farm{0}.staticflickr.com/{1}/{2}_{3}.jpg", url_builder_for_farm2_valueXML.InnerXml,
                                                             url_builder_for_photo_serverID2_value_from_XML.InnerXml, url_builder_for_photo_ID2_valueXML.InnerXml,
                                                             url_builder_for_photo_secret2_value_from_XML.InnerXml)
            Dim url_PhotoRequest3 As String = String.Format("https://farm{0}.staticflickr.com/{1}/{2}_{3}.jpg", url_builder_for_farm3_valueXML.InnerXml,
                                                             url_builder_for_photo_serverID3_value_from_XML.InnerXml, url_builder_for_photo_ID3_valueXML.InnerXml,
                                                             url_builder_for_photo_secret3_value_from_XML.InnerXml)
            
            ''render this image to an image box to be displayed on a bing maps api. Or look into using Flickrs maps 

            '"Copy and paste this link into a browser and see what photo is returned " + url_PhotoRequest)
            '' tried to display to messagebox , but could not click on its contents. 

            Dim PhotoRequest1 As WebRequest = WebRequest.Create(url_PhotoRequest1)
            Dim photoResponseStream As Stream = PhotoRequest1.GetResponse.GetResponseStream()
            Dim flickrPhotoFromStream As Image = Image.FromStream(photoResponseStream)
            PictureBox1.Image = flickrPhotoFromStream

            Dim PhotoRequest2 As WebRequest = WebRequest.Create(url_PhotoRequest2)
            Dim photoResponseStream2 As Stream = PhotoRequest2.GetResponse.GetResponseStream()
            Dim flickrPhotoFromStream2 As Image = Image.FromStream(photoResponseStream2)
            PictureBox2.Image = flickrPhotoFromStream2

            Dim PhotoRequest3 As WebRequest = WebRequest.Create(url_PhotoRequest3)
            Dim photoResponseStream3 As Stream = PhotoRequest3.GetResponse.GetResponseStream()
            Dim flickrPhotoFromStream3 As Image = Image.FromStream(photoResponseStream3)
            PictureBox3.Image = flickrPhotoFromStream3


            'error 404 page not found 
            '' need a try catch block when i reach this point. 
            ''todo get the rest of the elements from the initial response. 


            ''NOTE: each web request sent generates a different response of results. 


            'catch any exceptions and let me know what is going wrong. 
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    'Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
    '' create a web request for the authentication and get an access token to create an initial request 
    '    Try
    '        ' Dim tokenRequest As WebRequest = WebRequest.Create("https://api.flickr.com/services/rest/?method=flickr.test.echo&api_key=32036da6f917a5a5bf879ce5ba1b6863&format=rest&auth_token=72157651870085376-429a63a14d709dcb&api_sig=7034a717fe5c4d991bdb4c8a641ba986")

    '        Dim f As Flickr = New Flickr("4f60a04f101ef604ead9be84856d9519")

    '        MessageBox.Show("Authentication successful")
    '    Catch authenticationProblem As Exception
    '        MessageBox.Show(authenticationProblem.Message)
    '    End Try
    'End Sub
End Class
