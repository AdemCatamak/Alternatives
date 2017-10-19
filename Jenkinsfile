node('master'){
    stage('Clean and Checkout')
    {
        try { checkout scm } 
        catch(caughtError) { deleteDir(); checkout scm }
    }
    stage('Run') 
    {
      posh "./build.ps1 -experimental"
    } 
}

def posh(cmd) 
{
  bat 'powershell.exe -NoProfile -ExecutionPolicy Bypass -Command "& ' + cmd + '"'
}
