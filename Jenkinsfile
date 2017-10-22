node('master'){
    stage('Clean and Checkout')
    {
        try { checkout scm } 
        catch(caughtError) { deleteDir(); checkout scm }
    }
    stage('Run') 
    {
      sh('build.sh')
    } 
}
