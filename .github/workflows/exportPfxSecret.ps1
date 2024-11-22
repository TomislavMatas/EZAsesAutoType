# Generate a signing certificate in the Windows Application
# Packaging Project or add an existing signing certificate to the project.
# Next, use PowerShell to encode the .pfx file using Base64 encoding
# by running the following Powershell script to generate the output string:
$pfx_cert = Get-Content '../../cert/MatasConsultingSelfSigned.pfx' -Encoding Byte
[System.Convert]::ToBase64String($pfx_cert) | Out-File '../../cert/MatasConsultingSelfSigned_Encoded.txt'
