_now=$(date +"%m_%d_%Y_")
echo $_now
mongodump --out /opt/backup/dbBack
echo "finish db dump"
tar cvzf /opt/backup/$_now.Files.tar.gz /opt/appDir/deployed/public/img/news /opt/appDir/deployed/public/docs
echo "finish files gz"
tar cvzf /opt/backups/$_now.DB.tar.gz /opt/backups/dbBack
echo "finish db gz"
rm -rf /opt/backups/dbBack
echo "finish cleanup"
