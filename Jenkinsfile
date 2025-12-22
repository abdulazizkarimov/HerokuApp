pipeline {
    agent {
        docker {
            image 'mcr.microsoft.com/dotnet/sdk:8.0'
            args '--user root'
        }
    }

    environment {
        DOTNET_CLI_TELEMETRY_OPTOUT = '1'
        DOTNET_SKIP_FIRST_TIME_EXPERIENCE = '1'
        TEST_RESULTS = 'TestResults'
    }

    stages {

        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Restore') {
            steps {
                sh 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                sh 'dotnet build --configuration Release --no-restore'
            }
        }

        stage('Run NUnit Tests') {
            steps {
                sh '''
                    dotnet test \
                      --configuration Release \
                      --no-build \
                      --logger "trx;LogFileName=test_results.trx" \
                      --results-directory ${TEST_RESULTS}
                '''
            }
        }
    }

    post {
        always {
            junit allowEmptyResults: true, testResults: '**/TestResults/*.trx'
            archiveArtifacts artifacts: '**/TestResults/**/*', allowEmptyArchive: true
        }

        success {
            echo 'Tests passed'
        }

        failure {
            echo 'Tests failed'
        }
    }
}
