node('master'){
    stage('Clean and Checkout')
    {
        try { checkout scm } 
        catch(caughtError) { deleteDir(); checkout scm }
    }
    stage('Run') 
    {
        steps
        {
            echo 'Jenkins starts to executing build.sh'
            sh './build.sh'
            echo 'Jenkins completes to executing build.sh'
        }
    } 
}
