Imports System.IO
Imports System.Net
Imports System.Xml

Public Class flickRportion
    '' here is where are the flickr interation will be 

    '' for flikr i will need a web request for the city and state search built on these qualities. 

    '' this returns the string built ready to be sent. NO ERROR CHECKING PRIOR. 
    Private Function searchSettings() As String
        ' use this base url to generate a request. Replace api_ key with my api key 
        '       URL: https://api.flickr.com/services/rest/?method=flickr.photos.search
        '&api_key=2c33c4c48c9fa2f189c0c08e45be9322
        '&tags=St.+Louis%2C+mo+%2C+landmark%2C+landmarks%2C+neat%2C+places+to+go
        '&has_geo=1
        '&per_page=6&page=1
        '&format=rest
        Try
            Dim baseURL As String = "https://api.flickr.com/services/rest/?method=flickr.photos.search"
            Dim apiKey As String = "&api_key=efdd3ce6e251f57b82269e4f3855755b"
            Dim searchTags As String = "&tags=St.+Louis%2C+mo+%2C+landmark%2C+landmarks%2C+neat%2C+places+to+go"
            Dim getPhotos_with_geoLoations = "&has_geo=1"
            Dim photosPerPage = "&per_page=6&page=1"
            Dim responseFormat = "&format=rest"

            '' this string built represents all the components needed for the form. some defined. some derived from user input. 
            Dim stringBuilt As String = (baseURL + apiKey + searchTags + getPhotos_with_geoLoations + photosPerPage + responseFormat)
            Return stringBuilt
            Console.WriteLine("Here is the string built: youll find this under output " + stringBuilt)
            'MessageBox.Show("Here is the string built " + stringBuilt)
        Catch ex As Exception
            MessageBox.Show("this problem was thrown while trying to create a web request. Maybe missing a city or state.")
        End Try
    End Function
    '' this does not return an exception on all paths. WHY?


    '' this function needs access to the search settings because that is what they need to be sent as. May want to consider renaming. 
    Private Function send_request_for_photos(searchsettings As String) As Stream
        '' this function will return the photos as a stream because that is how i want to read them in order to be able to print to each photo box 

        ' create the web request first 
        Dim User_request As String = searchsettings

        Dim flickrInitialrequest As WebRequest = WebRequest.Create(User_request)



        '' step 1 create the stream from photos 
        Dim xmlResponse As Stream = flickrInitialrequest.GetResponse.GetResponseStream()

        Dim xmlweatherReader As New XmlDocument
        xmlweatherReader.Load(xmlResponse)




        '' save the response to a document called.. and read it 
        '' get the filewriter
        Dim file As System.IO.StreamWriter
        '' write the file to a file with the file name 
        file = My.Computer.FileSystem.OpenTextFileWriter("C:\Desktop\FirstBreakPoint.txt", True)
        file.WriteLine(xmlResponse)
        file.Close()
        '' now save to a file to see what is being written and also confirm. then search through and sort the info needed with the next loop to be created



        '' todo create the loop that loops through the data and saves/ maps them. Think about using a hashtable or hashmap. It could be useful here plus i need the practice.



    End Function


End Class
