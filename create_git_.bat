git init
git remote add origin https://github.com/baodev01/TaskList.git
git pull
git branch branch_20180322
git checkout branch_20180322
git branch --set-upstream-to=origin/branch_20180322
git pull
#git ls-files --others --exclude-from=.git/info/exclude *.bat
git config core.autocrlf true