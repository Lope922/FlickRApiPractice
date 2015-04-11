Imports System.Net
Imports System.IO
Imports FlickrNet
Imports SimpleOAuth
Imports System.Xml


Public Class Form1
    '' FLCIKR API KEY 4f60a04f101ef604ead9be84856d9519
    '' FLICKR API SECRET 4e3fc31dff403d28



    Dim apiKey As String = "4f60a04f101ef604ead9be84856d9519"

    Dim apiSecret As String = "4e3fc31dff403d28"

    Dim searchOptions As New PhotoSearchOptions

    Dim searchCity As String = "Minneapolis"

    Dim flickrThis As New Flickr(apiKey, apiSecret)

    Function OAuthRequestToken() As String
        Dim tempToken As String = ""
        tempToken.GetType()
        Return tempToken
    End Function


   
    Public Function PhotosSearch(searchCity As String) As PhotoCollection



    End Function


    '   Calling this method will also clear AuthToken and set OAuthAccessToken and OAuthAccessTokenSecret. 
    Public Function AuthOAuthGetAccessToken() As OAuthAccessToken
     
    End Function


    Private Sub getFlickrInfoBtn_Click(sender As Object, e As EventArgs) Handles getFlickrInfoBtn.Click
        Dim photoSearchOptions As New PhotoSearchOptions()
        photoSearchOptions.Tags = "St. Louis"
        photoSearchOptions.Page = 1
        Dim cityPhotos As Photo = flickrThis.PhotosSearch(photoSearchOptions.Tags)
        OAuthRequestToken(CType(apiKey))
        MessageBox.Show("Here is the token" + OAuthRequestToken().ToString)
        AuthOAuthGetAccessToken()
        Try
            If flickrThis.IsAuthenticated() = True Then
                MessageBox.Show("Authentication is true")
            Else
                MessageBox.Show("Authentication is false")
            End If

            'flickrThis.TestEcho(apiKey)
            ' flickrThis.AuthOAuthCheckToken()
            flickrThis.PhotosSearch(photoSearchOptions)
        Catch authenticationProblem As AuthenticationRequiredException
            MessageBox.Show(authenticationProblem.Message)
        End Try



    End Sub
End Class
